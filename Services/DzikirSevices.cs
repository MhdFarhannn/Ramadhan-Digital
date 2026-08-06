using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class DzikirServices
    {
        private readonly Database db;

        public DzikirServices(Database database)
        {
            db = database;
        }

        // GET ALL DZIKIR
        public async Task<IEnumerable<DzikirSetelahSholat>> GetAllAsync()
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    id,
                    nama AS Nama,
                    arabic AS Arabic,
                    terjemah AS Terjemah,
                    sumber AS Sumber
                FROM dzikir_setelah_sholat
                ORDER BY id ASC
            ";
            return await conn.QueryAsync<DzikirSetelahSholat>(sql);
        }

        // GET DZIKIR BY ID
        public async Task<DzikirSetelahSholat?> GetByIdAsync(int id)
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    id,
                    nama AS Nama,
                    arabic AS Arabic,
                    terjemah AS Terjemah,
                    sumber AS Sumber
                FROM dzikir_setelah_sholat
                WHERE id = @Id
            ";
            return await conn.QueryFirstOrDefaultAsync<DzikirSetelahSholat>(
                sql,
                new { Id = id }
            );
        }

        // CREATE DZIKIR
        public async Task<bool> CreateAsync(DzikirSetelahSholat dzikir)
        {
            using var conn = db.Connect();
            string sql = @"
                INSERT INTO dzikir_setelah_sholat (nama, arabic, terjemah, sumber)
                VALUES (@Nama, @Arabic, @Terjemah, @Sumber)
            ";
            var result = await conn.ExecuteAsync(sql, new
            {
                dzikir.Nama,
                dzikir.Arabic,
                dzikir.Terjemah,
                dzikir.Sumber
            });
            return result > 0;
        }

        // UPDATE DZIKIR
        public async Task<bool> UpdateAsync(int id, DzikirSetelahSholat dzikir)
        {
            using var conn = db.Connect();
            string sql = @"
                UPDATE dzikir_setelah_sholat
                SET nama = @Nama, arabic = @Arabic, terjemah = @Terjemah, sumber = @Sumber
                WHERE id = @Id
            ";
            var result = await conn.ExecuteAsync(sql, new
            {
                dzikir.Id,
                dzikir.Nama,
                dzikir.Arabic,
                dzikir.Terjemah,
                dzikir.Sumber
            });
            return result > 0;
        }

        // DELETE DZIKIR
        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = db.Connect();
            string sql = "DELETE FROM dzikir_setelah_sholat WHERE id = @Id";
            var result = await conn.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}