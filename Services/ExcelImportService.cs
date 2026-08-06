using ClosedXML.Excel;
using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class ExcelImportService
    {
        private readonly Database db;
        private readonly IPasswordService passwordService;

        // Hardcode constants untuk role
        private const int ROLE_SISWA = 3;  // Santri
        private const int ROLE_GURU = 2;   // Guru

        public ExcelImportService(Database database, IPasswordService passwordService)
        {
            db = database;
            this.passwordService = passwordService;
        }

        /// <summary>
        /// Import siswa dari Excel. IdRole di-hardcode = 3 (Santri)
        /// Excel format: Nama | Username | Password (kolom IdKelas tidak perlu, dari frontend)
        /// </summary>
        public async Task<(bool success, int importedCount, List<string> errors)> ImportSiswaFromExcel(Stream excelStream, int idKelas)
        {
            var errors = new List<string>();
            var users = new List<User>();
            int rowNumber = 1;

            try
            {
                using (var workbook = new XLWorkbook(excelStream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed();

                    var rowsList = rows.ToList();
                    if (rowsList.Count <= 1)
                    {
                        return (false, 0, new List<string> { "File Excel kosong atau hanya memiliki header" });
                    }

                    foreach (var row in rowsList.Skip(1))
                    {
                        rowNumber++;
                        try
                        {
                            var user = ParseSiswaFromExcelRow(row, rowNumber, idKelas);
                            if (user != null)
                            {
                                user.Password = passwordService.HashPassword(user.Password);
                                users.Add(user);
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Baris {rowNumber}: {ex.Message}");
                        }
                    }
                }

                if (users.Count == 0)
                {
                    return (false, 0, errors.Count > 0 ? errors : new List<string> { "Tidak ada data siswa yang valid untuk diimpor" });
                }

                int importedCount = await SaveUsersToDatabase(users, errors);
                return (importedCount > 0, importedCount, errors);
            }
            catch (Exception ex)
            {
                errors.Insert(0, $"Error membaca file Excel: {ex.Message}");
                return (false, 0, errors);
            }
        }

        /// <summary>
        /// Import guru dari Excel. IdRole di-hardcode = 2 (Guru)
        /// Excel format: Nama | Username | Password
        /// </summary>
        public async Task<(bool success, int importedCount, List<string> errors)> ImportGuruFromExcel(Stream excelStream)
        {
            var errors = new List<string>();
            var users = new List<User>();
            int rowNumber = 1;

            try
            {
                using (var workbook = new XLWorkbook(excelStream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed();

                    var rowsList = rows.ToList();
                    if (rowsList.Count <= 1)
                    {
                        return (false, 0, new List<string> { "File Excel kosong atau hanya memiliki header" });
                    }

                    foreach (var row in rowsList.Skip(1))
                    {
                        rowNumber++;
                        try
                        {
                            var user = ParseGuruFromExcelRow(row, rowNumber);
                            if (user != null)
                            {
                                user.Password = passwordService.HashPassword(user.Password);
                                users.Add(user);
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Baris {rowNumber}: {ex.Message}");
                        }
                    }
                }

                if (users.Count == 0)
                {
                    return (false, 0, errors.Count > 0 ? errors : new List<string> { "Tidak ada data guru yang valid untuk diimpor" });
                }

                int importedCount = await SaveUsersToDatabase(users, errors);
                return (importedCount > 0, importedCount, errors);
            }
            catch (Exception ex)
            {
                errors.Insert(0, $"Error membaca file Excel: {ex.Message}");
                return (false, 0, errors);
            }
        }

        /// <summary>
        /// Parse siswa dari Excel row. IdRole dan IdKelas di-set otomatis
        /// </summary>
        private User? ParseSiswaFromExcelRow(IXLRangeRow row, int rowNumber, int idKelas)
        {
            try
            {
                var nama = GetCellValue(row, 1)?.Trim();
                var username = GetCellValue(row, 2)?.Trim();
                var password = GetCellValue(row, 3)?.Trim();

                if (string.IsNullOrWhiteSpace(nama))
                    throw new Exception("Kolom 'Nama' tidak boleh kosong");
                if (string.IsNullOrWhiteSpace(username))
                    throw new Exception("Kolom 'Username' tidak boleh kosong");
                if (string.IsNullOrWhiteSpace(password))
                    throw new Exception("Kolom 'Password' tidak boleh kosong");

                return new User
                {
                    Nama = nama,
                    Username = username,
                    Password = password,
                    IdRole = ROLE_SISWA,      // Hardcode: Santri = 3
                    IdKelas = idKelas         // Dari parameter (dropdown frontend)
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses baris: {ex.Message}");
            }
        }

        /// <summary>
        /// Parse guru dari Excel row. IdRole di-set otomatis, IdKelas null
        /// </summary>
        private User? ParseGuruFromExcelRow(IXLRangeRow row, int rowNumber)
        {
            try
            {
                var nama = GetCellValue(row, 1)?.Trim();
                var username = GetCellValue(row, 2)?.Trim();
                var password = GetCellValue(row, 3)?.Trim();

                if (string.IsNullOrWhiteSpace(nama))
                    throw new Exception("Kolom 'Nama' tidak boleh kosong");
                if (string.IsNullOrWhiteSpace(username))
                    throw new Exception("Kolom 'Username' tidak boleh kosong");
                if (string.IsNullOrWhiteSpace(password))
                    throw new Exception("Kolom 'Password' tidak boleh kosong");

                return new User
                {
                    Nama = nama,
                    Username = username,
                    Password = password,
                    IdRole = ROLE_GURU,       // Hardcode: Guru = 2
                    IdKelas = null            // Guru tidak memiliki kelas
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses baris: {ex.Message}");
            }
        }

        private string? GetCellValue(IXLRangeRow row, int columnNumber)
        {
            var cell = row.Cell(columnNumber);
            if (cell == null)
                return null;

            var value = cell.Value;
            if (value.IsBlank)
                return null;

            return value.ToString();
        }

        private async Task<int> SaveUsersToDatabase(List<User> users, List<string> errors)
        {
            int successCount = 0;
            using var conn = db.Connect();

            foreach (var user in users)
            {
                try
                {
                    string checkSql = "SELECT COUNT(*) FROM users WHERE username = @Username";
                    int existingCount = await conn.ExecuteScalarAsync<int>(checkSql, new { Username = user.Username });

                    if (existingCount > 0)
                    {
                        errors.Add($"Username '{user.Username}' sudah terdaftar");
                        continue;
                    }

                    string sql = @"
                        INSERT INTO users
                        (id_role, id_kelas, nama, username, password)
                        VALUES
                        (@IdRole, @IdKelas, @Nama, @Username, @Password)
                    ";

                    int result = await conn.ExecuteAsync(sql, new
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
                    errors.Add($"Error menyimpan user '{user.Username}': {ex.Message}");
                }
            }

            return successCount;
        }
    }
}
