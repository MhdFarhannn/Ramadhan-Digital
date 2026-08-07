using Dapper;
using Microsoft.VisualBasic;
using Ramadhan_Digital.Models;
namespace Ramadhan_Digital.Services
{
    public class KelasServices
    {
        private readonly Database db;

        public KelasServices(Database database)
        {
            db = database;
        }
         public async Task<IEnumerable<Kelas>> GetAllAsync()
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    id,
                    nama AS Nama,
                    angkatan AS Angkatan
                FROM kelas
                ORDER BY id ASC
            ";
            return await conn.QueryAsync<Kelas>(sql);
        }

        public async Task<Kelas?> GetByIdAsync(int id)
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    id,
                    nama AS Nama,
                    angkatan AS Angkatan
                FROM kelas
                WHERE id = @Id
            ";
            return await conn.QueryFirstOrDefaultAsync<Kelas>(
                sql,
                new { Id = id }
            );
        }

        public async Task<bool> CreateAsync(Kelas kelas)
        {
            using var conn = db.Connect();
            string sql = @"
                INSERT INTO kelas (nama, angkatan)
                VALUES (@Nama, @Angkatan)
            ";
            var result = await conn.ExecuteAsync(sql, kelas);
            return result > 0;
        }


    }
}
