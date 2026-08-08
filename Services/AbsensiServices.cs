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

        // 1. GET REKAP / FORM ABSENSI PER KELAS
        public async Task<IEnumerable<dynamic>> GetAbsensiByKelasAndDateAsync(int idKelas, DateTime tanggal)
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT 
                    u.id AS IdUser,
                    u.nama_lengkap AS NamaSiswa,
                    @Tanggal::date AS Tanggal,
                    COALESCE(sa.nama, 'Belum Absen') AS StatusAbsensi,
                    COALESCE(a.id_status_absensi, 0) AS IdStatusAbsensi
                FROM users u
                LEFT JOIN absensi a ON u.id = a.id_user AND a.tanggal = @Tanggal::date
                LEFT JOIN status_absensi sa ON a.id_status_absensi = sa.id
                WHERE u.id_kelas = @IdKelas AND u.id_role = 2
                ORDER BY u.nama_lengkap ASC;
            ";

            return await conn.QueryAsync(sql, new { IdKelas = idKelas, Tanggal = tanggal.Date });
        }

        // 2. SIMPAN / UPDATE ABSENSI MASSAL OLEH GURU (BATCH UPSERT)
        public async Task<bool> SaveAbsensiKelasAsync(DateTime tanggal, List<DetailAbsensiSiswa> listAbsensi)
        {
            using var conn = db.Connect();
            using var transaction = conn.BeginTransaction();

            try
            {
                foreach (var item in listAbsensi)
                {
                    // Cek apakah data absensi siswa ini sudah ada di tanggal tsb
                    string checkSql = "SELECT id FROM absensi WHERE id_user = @IdUser AND tanggal = @Tanggal::date;";
                    var existingId = await conn.QueryFirstOrDefaultAsync<int?>(
                        checkSql,
                        new { IdUser = item.IdUser, Tanggal = tanggal.Date },
                        transaction
                    );

                    if (existingId.HasValue)
                    {
                        // Update
                        string updateSql = @"
                            UPDATE absensi 
                            SET id_status_absensi = @IdStatusAbsensi 
                            WHERE id = @Id;";

                        await conn.ExecuteAsync(
                            updateSql,
                            new { IdStatusAbsensi = item.IdStatusAbsensi, Id = existingId.Value },
                            transaction
                        );
                    }
                    else
                    {
                        // Insert Baru
                        string insertSql = @"
                            INSERT INTO absensi (id_user, tanggal, id_status_absensi)
                            VALUES (@IdUser, @Tanggal::date, @IdStatusAbsensi);";

                        await conn.ExecuteAsync(
                            insertSql,
                            new { IdUser = item.IdUser, Tanggal = tanggal.Date, IdStatusAbsensi = item.IdStatusAbsensi },
                            transaction
                        );
                    }
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }

    public class DetailAbsensiSiswa
    {
        public int IdUser { get; set; }
        public int IdStatusAbsensi { get; set; }
    }
}