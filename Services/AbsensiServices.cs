using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class AbsensiServices
    {
        private readonly Database db;

        public AbsensiServices(Database database)
        {
            db = database;
        }

        // ============================================================
        // GET ABSENSI SISWA BERDASARKAN KELAS + TANGGAL
        // ============================================================

        public async Task<IEnumerable<dynamic>> GetAbsensiByKelasAndDateAsync(
            int idKelas,
            DateOnly tanggal)
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT
                    u.id AS IdUser,
                    u.nama AS NamaSiswa,
                    @Tanggal AS Tanggal,

                    COALESCE(
                        sa.nama,
                        'Belum Absen'
                    ) AS StatusAbsensi,

                    COALESCE(
                        a.id_status_absensi,
                        0
                    ) AS IdStatusAbsensi

                FROM users u

                LEFT JOIN absensi a
                    ON u.id = a.id_user
                    AND a.tanggal = @Tanggal

                LEFT JOIN status_absensi sa
                    ON a.id_status_absensi = sa.id

                WHERE
                    u.id_kelas = @IdKelas
                    AND u.id_role = 3

                ORDER BY u.nama ASC;
            ";

            return await conn.QueryAsync(
                sql,
                new
                {
                    IdKelas = idKelas,
                    Tanggal = tanggal
                }
            );
        }

        // ============================================================
        // CEK APAKAH KELAS SUDAH ABSEN HARI INI
        //
        // Tabel absensi TIDAK memiliki id_kelas.
        //
        // Relasi:
        //
        // absensi.id_user
        //       ↓
        // users.id
        //       ↓
        // users.id_kelas
        //
        // ============================================================

        public async Task<bool> IsAbsensiKelasSudahAdaAsync(
            int idKelas,
            DateOnly tanggal)
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM absensi a

                    INNER JOIN users u
                        ON u.id = a.id_user

                    WHERE
                        u.id_kelas = @IdKelas
                        AND a.tanggal = @Tanggal
                );
            ";

            return await conn.ExecuteScalarAsync<bool>(
                sql,
                new
                {
                    IdKelas = idKelas,
                    Tanggal = tanggal
                }
            );
        }

        // ============================================================
        // SIMPAN ABSENSI MASSAL
        //
        // RULE:
        // 1 kelas hanya dapat diinput 1 kali dalam 1 hari.
        // ============================================================

        public async Task<bool> SaveAbsensiKelasAsync(
            int idKelas,
            DateOnly tanggal,
            List<DetailAbsensiSiswa> listAbsensi)
        {
            using var conn = db.Connect();

            using var transaction = conn.BeginTransaction();

            try
            {
                // ====================================================
                // CEK ULANG DI DALAM TRANSACTION
                // ====================================================

                string checkSql = @"
                    SELECT EXISTS (
                        SELECT 1
                        FROM absensi a

                        INNER JOIN users u
                            ON u.id = a.id_user

                        WHERE
                            u.id_kelas = @IdKelas
                            AND a.tanggal = @Tanggal
                    );
                ";

                var sudahAda =
                    await conn.ExecuteScalarAsync<bool>(
                        checkSql,
                        new
                        {
                            IdKelas = idKelas,
                            Tanggal = tanggal
                        },
                        transaction
                    );

                if (sudahAda)
                {
                    transaction.Rollback();

                    return false;
                }

                // ====================================================
                // VALIDASI SEMUA SISWA
                //
                // Pastikan semua IdUser:
                // - memang ada
                // - merupakan siswa
                // - berasal dari kelas yang sama
                // ====================================================

                string validateStudentSql = @"
                    SELECT COUNT(*)
                    FROM users
                    WHERE
                        id = @IdUser
                        AND id_kelas = @IdKelas
                        AND id_role = 3;
                ";

                foreach (var item in listAbsensi)
                {
                    var isStudentValid =
                        await conn.ExecuteScalarAsync<int>(
                            validateStudentSql,
                            new
                            {
                                IdUser = item.IdUser,
                                IdKelas = idKelas
                            },
                            transaction
                        );

                    if (isStudentValid == 0)
                    {
                        transaction.Rollback();

                        return false;
                    }
                }

                // ====================================================
                // INSERT ABSENSI
                // ====================================================

                string insertSql = @"
                    INSERT INTO absensi
                    (
                        id_user,
                        tanggal,
                        id_status_absensi
                    )
                    VALUES
                    (
                        @IdUser,
                        @Tanggal,
                        @IdStatusAbsensi
                    );
                ";

                foreach (var item in listAbsensi)
                {
                    await conn.ExecuteAsync(
                        insertSql,
                        new
                        {
                            IdUser = item.IdUser,
                            Tanggal = tanggal,
                            IdStatusAbsensi = item.IdStatusAbsensi
                        },
                        transaction
                    );
                }

                // ====================================================
                // COMMIT
                // ====================================================

                transaction.Commit();

                return true;
            }
            catch
            {
                transaction.Rollback();

                return false;
            }
        }

        // ============================================================
        // GET REKAP ABSENSI KELAS
        // ============================================================

        public async Task<IEnumerable<Absensi>> GetRekapAbsensiKelasAsync(
            int idKelas)
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT
                    a.id AS Id,
                    a.id_user AS IdUser,
                    a.tanggal AS Tanggal,
                    a.id_status_absensi AS IdStatusAbsensi

                FROM absensi a

                INNER JOIN users u
                    ON u.id = a.id_user

                WHERE
                    u.id_kelas = @IdKelas

                ORDER BY
                    a.tanggal DESC,
                    a.id_user ASC;
            ";

            return await conn.QueryAsync<Absensi>(
                sql,
                new
                {
                    IdKelas = idKelas
                }
            );
        }
    }

    // ================================================================
    // DETAIL ABSENSI SISWA
    // ================================================================

    public class DetailAbsensiSiswa
    {
        public int IdUser { get; set; }

        public int IdStatusAbsensi { get; set; }
    }
}
