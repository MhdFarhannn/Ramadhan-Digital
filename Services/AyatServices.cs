using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class AyatServices
    {
        private readonly Database db;

        public AyatServices(Database database)
        {
            db = database;
        }


        // GET ALL AYAT
        public async Task<IEnumerable<Ayat>> GetAllAsync()
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT 
                    id,
                    id_surah AS IdSurah,
                    nomor AS Nomor,
                    arab AS Arab,
                    terjemah AS Terjemah
                FROM ayat
                ORDER BY id_surah ASC, nomor ASC
            ";

            return await conn.QueryAsync<Ayat>(sql);
        }


        // GET AYAT BY ID
        public async Task<Ayat?> GetByIdAsync(int id)
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT 
                    id,
                    id_surah AS IdSurah,
                    nomor AS Nomor,
                    arab AS Arab,
                    terjemah AS Terjemah
                FROM ayat
                WHERE id = @Id
            ";

            return await conn.QueryFirstOrDefaultAsync<Ayat>(
                sql,
                new { Id = id }
            );
        }


        // GET AYAT BY SURAH
        public async Task<IEnumerable<Ayat>> GetBySurahAsync(int idSurah)
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT 
                    id,
                    id_surah AS IdSurah,
                    nomor AS Nomor,
                    arab AS Arab,
                    terjemah AS Terjemah
                FROM ayat
                WHERE id_surah = @IdSurah
                ORDER BY nomor ASC
            ";

            return await conn.QueryAsync<Ayat>(
                sql,
                new { IdSurah = idSurah }
            );
        }


        // GET AYAT BY SURAH DAN NOMOR
        public async Task<Ayat?> GetBySurahAndNomorAsync(int idSurah, int nomor)
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT 
                    id,
                    id_surah AS IdSurah,
                    nomor AS Nomor,
                    arab AS Arab,
                    terjemah AS Terjemah
                FROM ayat
                WHERE id_surah = @IdSurah AND nomor = @Nomor
            ";

            return await conn.QueryFirstOrDefaultAsync<Ayat>(
                sql,
                new { IdSurah = idSurah, Nomor = nomor }
            );
        }


        // CREATE AYAT
        public async Task<bool> CreateAsync(Ayat ayat)
        {
            using var conn = db.Connect();

            string sql = @"
                INSERT INTO ayat
                (
                    id_surah,
                    nomor,
                    arab,
                    terjemah
                )
                VALUES
                (
                    @IdSurah,
                    @Nomor,
                    @Arab,
                    @Terjemah
                )
            ";

            var result = await conn.ExecuteAsync(sql, new
            {
                ayat.IdSurah,
                ayat.Nomor,
                ayat.Arab,
                ayat.Terjemah
            });

            return result > 0;
        }


        // UPDATE AYAT
        public async Task<bool> UpdateAsync(Ayat ayat)
        {
            using var conn = db.Connect();

            string sql = @"
                UPDATE ayat
                SET
                    id_surah = @IdSurah,
                    nomor = @Nomor,
                    arab = @Arab,
                    terjemah = @Terjemah
                WHERE id = @Id
            ";

            var result = await conn.ExecuteAsync(sql, new
            {
                ayat.Id,
                ayat.IdSurah,
                ayat.Nomor,
                ayat.Arab,
                ayat.Terjemah
            });

            return result > 0;
        }


        // DELETE AYAT
        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = db.Connect();

            string sql = @"
                DELETE FROM ayat
                WHERE id = @Id
            ";

            var result = await conn.ExecuteAsync(
                sql,
                new { Id = id }
            );

            return result > 0;
        }
    }
}