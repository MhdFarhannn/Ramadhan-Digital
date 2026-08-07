using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class BacaanSholatServices
    {
        private readonly Database db;

        public BacaanSholatServices(Database database)
        {
            db = database;
        }

        // GET ALL BACAAN SHOLAT
        public async Task<IEnumerable<BacaanSholat>> GetAllAsync()
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    id,
                    id_hukum AS IdHukum,
                    urutan AS Urutan,
                    nama AS Nama,
                    gerakan AS Gerakan,
                    arabic AS Arabic,
                    translate AS Translate
                FROM bacaan_sholat
                ORDER BY urutan ASC
            ";
            return await conn.QueryAsync<BacaanSholat>(sql);
        }

        // GET BACAAN SHOLAT BY ID
        public async Task<BacaanSholat?> GetByIdAsync(int id)
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    id,
                    id_hukum AS IdHukum,
                    urutan AS Urutan,
                    nama AS Nama,
                    gerakan AS Gerakan,
                    arabic AS Arabic,
                    translate AS Translate
                FROM bacaan_sholat
                WHERE id = @Id
            ";
            return await conn.QueryFirstOrDefaultAsync<BacaanSholat>(
                sql,
                new { Id = id }
            );
        }

        // CREATE BACAAN SHOLAT
        public async Task<bool> CreateAsync(BacaanSholat bacaanSholat)
        {
            using var conn = db.Connect();
            string sql = @"
                INSERT INTO bacaan_sholat (id_hukum, urutan, nama, gerakan, arabic, translate)
                VALUES (@IdHukum, @Urutan, @Nama, @Gerakan, @Arabic, @Translate)
            ";
            var result = await conn.ExecuteAsync(sql, bacaanSholat);
            return result > 0;
        }
    }
}