using ClosedXML.Excel;
using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class ExcelImportService
    {
        private readonly Database db;
        private readonly IPasswordService passwordService;

        // Role ID
        private const int ROLE_SISWA = 3;
        private const int ROLE_GURU = 2;

        public ExcelImportService(
            Database database,
            IPasswordService passwordService)
        {
            db = database;
            this.passwordService = passwordService;
        }

        // ============================================================
        // IMPORT SISWA
        // ============================================================
        //
        // Format Excel:
        //
        // | Nama | Username | Password |
        // |------|----------|----------|
        // | Ahmad | ahmad01 | 123456 |
        // | Budi  | budi01  | 123456 |
        //
        // id_kelas TIDAK PERLU ADA DI EXCEL.
        // idKelas berasal dari pilihan kelas di frontend.
        //
        public async Task<(bool success, int importedCount, List<string> errors)>
            ImportSiswaFromExcel(Stream excelStream, int idKelas)
        {
            var errors = new List<string>();
            var users = new List<User>();

            int rowNumber = 1;

            try
            {
                // ====================================================
                // 1. VALIDASI ID KELAS
                // ====================================================

                using var conn = db.Connect();

                var kelasExists = await conn.ExecuteScalarAsync<int>(
                    @"
                    SELECT COUNT(*)
                    FROM kelas
                    WHERE id = @IdKelas
                    ",
                    new
                    {
                        IdKelas = idKelas
                    });

                if (kelasExists == 0)
                {
                    return (
                        false,
                        0,
                        new List<string>
                        {
                            $"Kelas dengan ID {idKelas} tidak ditemukan."
                        }
                    );
                }

                // ====================================================
                // 2. BACA FILE EXCEL
                // ====================================================

                using (var workbook = new XLWorkbook(excelStream))
                {
                    if (workbook.Worksheets.Count == 0)
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "File Excel tidak memiliki worksheet."
                            }
                        );
                    }

                    var worksheet = workbook.Worksheet(1);

                    var usedRange = worksheet.RangeUsed();

                    if (usedRange == null)
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "File Excel kosong."
                            }
                        );
                    }

                    var rows = usedRange.RowsUsed().ToList();

                    // =================================================
                    // 3. VALIDASI JUMLAH BARIS
                    // =================================================

                    if (rows.Count <= 1)
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "File Excel kosong atau hanya memiliki header."
                            }
                        );
                    }

                    // =================================================
                    // 4. VALIDASI HEADER
                    // =================================================

                    var headerRow = rows[0];

                    var headerNama =
                        GetCellValue(headerRow, 1)?.Trim();

                    var headerUsername =
                        GetCellValue(headerRow, 2)?.Trim();

                    var headerPassword =
                        GetCellValue(headerRow, 3)?.Trim();

                    if (!string.Equals(
                            headerNama,
                            "Nama",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "Kolom pertama harus bernama 'Nama'."
                            }
                        );
                    }

                    if (!string.Equals(
                            headerUsername,
                            "Username",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "Kolom kedua harus bernama 'Username'."
                            }
                        );
                    }

                    if (!string.Equals(
                            headerPassword,
                            "Password",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "Kolom ketiga harus bernama 'Password'."
                            }
                        );
                    }

                    // =================================================
                    // 5. CEK DUPLIKAT USERNAME DI DALAM EXCEL
                    // =================================================

                    var usernamesInExcel =
                        new HashSet<string>(
                            StringComparer.OrdinalIgnoreCase);

                    // =================================================
                    // 6. BACA DATA SISWA
                    // =================================================

                    foreach (var row in rows.Skip(1))
                    {
                        rowNumber++;

                        try
                        {
                            var user = ParseSiswaFromExcelRow(
                                row,
                                idKelas);

                            if (user == null)
                            {
                                continue;
                            }

                            // =========================================
                            // CEK DUPLIKAT USERNAME DI EXCEL
                            // =========================================

                            if (!usernamesInExcel.Add(user.Username))
                            {
                                errors.Add(
                                    $"Baris {rowNumber}: Username '{user.Username}' duplikat di dalam file Excel.");

                                continue;
                            }

                            // =========================================
                            // HASH PASSWORD
                            // =========================================

                            user.Password =
                                passwordService.HashPassword(
                                    user.Password);

                            users.Add(user);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(
                                $"Baris {rowNumber}: {ex.Message}");
                        }
                    }
                }

                // ====================================================
                // 7. TIDAK ADA DATA VALID
                // ====================================================

                if (users.Count == 0)
                {
                    if (errors.Count == 0)
                    {
                        errors.Add(
                            "Tidak ada data siswa yang valid untuk diimpor.");
                    }

                    return (
                        false,
                        0,
                        errors
                    );
                }

                // ====================================================
                // 8. SIMPAN KE DATABASE
                // ====================================================

                int importedCount =
                    await SaveUsersToDatabase(
                        users,
                        errors);

                // ====================================================
                // 9. HASIL
                // ====================================================

                return (
                    importedCount > 0,
                    importedCount,
                    errors
                );
            }
            catch (Exception ex)
            {
                errors.Insert(
                    0,
                    $"Error membaca file Excel: {ex.Message}");

                return (
                    false,
                    0,
                    errors
                );
            }
        }

        // ============================================================
        // IMPORT GURU
        // ============================================================

        public async Task<(bool success, int importedCount, List<string> errors)>
            ImportGuruFromExcel(Stream excelStream)
        {
            var errors = new List<string>();
            var users = new List<User>();

            int rowNumber = 1;

            try
            {
                using (var workbook = new XLWorkbook(excelStream))
                {
                    if (workbook.Worksheets.Count == 0)
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "File Excel tidak memiliki worksheet."
                            }
                        );
                    }

                    var worksheet = workbook.Worksheet(1);

                    var usedRange = worksheet.RangeUsed();

                    if (usedRange == null)
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "File Excel kosong."
                            }
                        );
                    }

                    var rows = usedRange.RowsUsed().ToList();

                    if (rows.Count <= 1)
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "File Excel kosong atau hanya memiliki header."
                            }
                        );
                    }

                    // =================================================
                    // VALIDASI HEADER
                    // =================================================

                    var headerRow = rows[0];

                    var headerNama =
                        GetCellValue(headerRow, 1)?.Trim();

                    var headerUsername =
                        GetCellValue(headerRow, 2)?.Trim();

                    var headerPassword =
                        GetCellValue(headerRow, 3)?.Trim();

                    if (!string.Equals(
                            headerNama,
                            "Nama",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "Kolom pertama harus bernama 'Nama'."
                            }
                        );
                    }

                    if (!string.Equals(
                            headerUsername,
                            "Username",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "Kolom kedua harus bernama 'Username'."
                            }
                        );
                    }

                    if (!string.Equals(
                            headerPassword,
                            "Password",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        return (
                            false,
                            0,
                            new List<string>
                            {
                                "Kolom ketiga harus bernama 'Password'."
                            }
                        );
                    }

                    // =================================================
                    // CEK DUPLIKAT USERNAME DI EXCEL
                    // =================================================

                    var usernamesInExcel =
                        new HashSet<string>(
                            StringComparer.OrdinalIgnoreCase);

                    // =================================================
                    // BACA DATA GURU
                    // =================================================

                    foreach (var row in rows.Skip(1))
                    {
                        rowNumber++;

                        try
                        {
                            var user =
                                ParseGuruFromExcelRow(row);

                            if (user == null)
                            {
                                continue;
                            }

                            // =========================================
                            // CEK DUPLIKAT USERNAME
                            // =========================================

                            if (!usernamesInExcel.Add(user.Username))
                            {
                                errors.Add(
                                    $"Baris {rowNumber}: Username '{user.Username}' duplikat di dalam file Excel.");

                                continue;
                            }

                            // =========================================
                            // HASH PASSWORD
                            // =========================================

                            user.Password =
                                passwordService.HashPassword(
                                    user.Password);

                            users.Add(user);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(
                                $"Baris {rowNumber}: {ex.Message}");
                        }
                    }
                }

                // ====================================================
                // TIDAK ADA DATA VALID
                // ====================================================

                if (users.Count == 0)
                {
                    if (errors.Count == 0)
                    {
                        errors.Add(
                            "Tidak ada data guru yang valid untuk diimpor.");
                    }

                    return (
                        false,
                        0,
                        errors
                    );
                }

                // ====================================================
                // SIMPAN KE DATABASE
                // ====================================================

                int importedCount =
                    await SaveUsersToDatabase(
                        users,
                        errors);

                return (
                    importedCount > 0,
                    importedCount,
                    errors
                );
            }
            catch (Exception ex)
            {
                errors.Insert(
                    0,
                    $"Error membaca file Excel: {ex.Message}");

                return (
                    false,
                    0,
                    errors
                );
            }
        }

        // ============================================================
        // PARSE SISWA
        // ============================================================

        private User? ParseSiswaFromExcelRow(
            IXLRangeRow row,
            int idKelas)
        {
            try
            {
                var nama =
                    GetCellValue(row, 1)?.Trim();

                var username =
                    GetCellValue(row, 2)?.Trim();

                var password =
                    GetCellValue(row, 3)?.Trim();

                // =============================================
                // VALIDASI NAMA
                // =============================================

                if (string.IsNullOrWhiteSpace(nama))
                {
                    throw new Exception(
                        "Kolom 'Nama' tidak boleh kosong.");
                }

                // =============================================
                // VALIDASI USERNAME
                // =============================================

                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new Exception(
                        "Kolom 'Username' tidak boleh kosong.");
                }

                // =============================================
                // VALIDASI PASSWORD
                // =============================================

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception(
                        "Kolom 'Password' tidak boleh kosong.");
                }

                // =============================================
                // BUAT USER SISWA
                // =============================================

                return new User
                {
                    Nama = nama,
                    Username = username,
                    Password = password,

                    // Role Siswa
                    IdRole = ROLE_SISWA,

                    // Dari pilihan kelas admin
                    IdKelas = idKelas
                };
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Gagal memproses data siswa: {ex.Message}");
            }
        }

        // ============================================================
        // PARSE GURU
        // ============================================================

        private User? ParseGuruFromExcelRow(
            IXLRangeRow row)
        {
            try
            {
                var nama =
                    GetCellValue(row, 1)?.Trim();

                var username =
                    GetCellValue(row, 2)?.Trim();

                var password =
                    GetCellValue(row, 3)?.Trim();

                // =============================================
                // VALIDASI NAMA
                // =============================================

                if (string.IsNullOrWhiteSpace(nama))
                {
                    throw new Exception(
                        "Kolom 'Nama' tidak boleh kosong.");
                }

                // =============================================
                // VALIDASI USERNAME
                // =============================================

                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new Exception(
                        "Kolom 'Username' tidak boleh kosong.");
                }

                // =============================================
                // VALIDASI PASSWORD
                // =============================================

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception(
                        "Kolom 'Password' tidak boleh kosong.");
                }

                // =============================================
                // BUAT USER GURU
                // =============================================

                return new User
                {
                    Nama = nama,
                    Username = username,
                    Password = password,

                    // Role Guru
                    IdRole = ROLE_GURU,

                    // Guru tidak memiliki kelas
                    IdKelas = null
                };
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Gagal memproses data guru: {ex.Message}");
            }
        }

        // ============================================================
        // GET CELL VALUE
        // ============================================================

        private string? GetCellValue(
            IXLRangeRow row,
            int columnNumber)
        {
            var cell = row.Cell(columnNumber);

            if (cell == null)
            {
                return null;
            }

            if (cell.Value.IsBlank)
            {
                return null;
            }

            return cell.Value.ToString();
        }

        // ============================================================
        // SAVE USERS TO DATABASE
        // ============================================================

        private async Task<int> SaveUsersToDatabase(
            List<User> users,
            List<string> errors)
        {
            int successCount = 0;

            using var conn = db.Connect();

            foreach (var user in users)
            {
                try
                {
                    // =============================================
                    // CEK USERNAME DI DATABASE
                    // =============================================

                    string checkSql = @"
                        SELECT COUNT(*)
                        FROM users
                        WHERE username = @Username;
                    ";

                    int existingCount =
                        await conn.ExecuteScalarAsync<int>(
                            checkSql,
                            new
                            {
                                Username = user.Username
                            });

                    if (existingCount > 0)
                    {
                        errors.Add(
                            $"Username '{user.Username}' sudah terdaftar.");

                        continue;
                    }

                    // =============================================
                    // INSERT USER
                    // =============================================

                    string sql = @"
                        INSERT INTO users
                        (
                            id_role,
                            id_kelas,
                            nama,
                            username,
                            password
                        )
                        VALUES
                        (
                            @IdRole,
                            @IdKelas,
                            @Nama,
                            @Username,
                            @Password
                        );
                    ";

                    int result =
                        await conn.ExecuteAsync(
                            sql,
                            new
                            {
                                IdRole = user.IdRole,
                                IdKelas = user.IdKelas,
                                Nama = user.Nama,
                                Username = user.Username,
                                Password = user.Password
                            });

                    if (result > 0)
                    {
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add(
                        $"Error menyimpan user '{user.Username}': {ex.Message}");
                }
            }

            return successCount;
        }
    }
}
