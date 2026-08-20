using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class TausiahServices
    {
        private readonly Database db;

        public TausiahServices(Database database)
        {
            db = database;
        }

        // GET ALL TAUSIAH
        public async Task<IEnumerable<Tausiah>> GetAllAsync()
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    id AS Id,
                    id_user AS IdUser,
                    judul_tausiah AS JudulTausiah,
                    ringkasan AS Ringkasan,
                    tanggal::timestamp AS Tanggal,
                    nama_penceramah AS NamaPenceramah
                FROM tausiah
                ORDER BY tanggal DESC
            ";
            return await conn.QueryAsync<Tausiah>(sql);
        }

        // GET TAUSIAH BY ID
        public async Task<Tausiah?> GetByIdAsync(int id)
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT  
                    id AS Id,
                    id_user AS IdUser,
                    judul_tausiah AS JudulTausiah,
                    ringkasan AS Ringkasan,
                    tanggal::timestamp AS Tanggal,
                    nama_penceramah AS NamaPenceramah   
                FROM tausiah
                WHERE id = @Id
            ";
            return await conn.QueryFirstOrDefaultAsync<Tausiah>(
                sql,
                new { Id = id }
            );
        }

        // CREATE TAUSIAH
        public async Task<bool> CreateAsync(Tausiah tausiah)
        {
            using var conn = db.Connect();
            string sql = @"
                INSERT INTO tausiah (id_user, judul_tausiah, ringkasan, tanggal, nama_penceramah)
                VALUES (@IdUser, @JudulTausiah, @Ringkasan, @Tanggal, @NamaPenceramah)
            ";
            var result = await conn.ExecuteAsync(sql, tausiah);
            return result > 0;
        }
    }
}