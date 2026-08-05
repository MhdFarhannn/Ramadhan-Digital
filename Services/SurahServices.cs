using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class SurahServices
    {
        private readonly Database db;

        public SurahServices(Database database)
        {
            db = database;
        }


        // GET ALL SURAH
        public async Task<IEnumerable<Surah>> GetAllAsync()
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT 
                    id,
                    surah AS SurahName,
                    artisurat AS ArtiSurat,
                    tempat_turun AS TempatTurun,
                    nomor AS Nomor
                FROM surah
                ORDER BY nomor ASC
            ";

            return await conn.QueryAsync<Surah>(sql);
        }


        // GET SURAH BY ID
        public async Task<Surah?> GetByIdAsync(int id)
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT 
                    id,
                    surah AS SurahName,
                    artisurat AS ArtiSurat,
                    tempat_turun AS TempatTurun,
                    nomor AS Nomor
                FROM surah
                WHERE id = @Id
            ";

            return await conn.QueryFirstOrDefaultAsync<Surah>(
                sql,
                new { Id = id }
            );
        }


        // GET SURAH BY NOMOR
        public async Task<Surah?> GetByNomorAsync(int nomor)
        {
            using var conn = db.Connect();

            string sql = @"
                SELECT 
                    id,
                    surah AS SurahName,
                    artisurat AS ArtiSurat,
                    tempat_turun AS TempatTurun,
                    nomor AS Nomor
                FROM surah
                WHERE nomor = @Nomor
            ";

            return await conn.QueryFirstOrDefaultAsync<Surah>(
                sql,
                new { Nomor = nomor }
            );
        }


        // CREATE SURAH
        public async Task<bool> CreateAsync(Surah surah)
        {
            using var conn = db.Connect();

            string sql = @"
                INSERT INTO surah
                (
                    surah,
                    artisurat,
                    tempat_turun,
                    nomor
                )
                VALUES
                (
                    @SurahName,
                    @ArtiSurat,
                    @TempatTurun,
                    @Nomor
                )
            ";

            var result = await conn.ExecuteAsync(sql, new
            {
                surah.SurahName,
                surah.ArtiSurat,
                surah.TempatTurun,
                surah.Nomor
            });

            return result > 0;
        }


        // UPDATE SURAH
        public async Task<bool> UpdateAsync(Surah surah)
        {
            using var conn = db.Connect();

            string sql = @"
                UPDATE surah
                SET
                    surah = @SurahName,
                    artisurat = @ArtiSurat,
                    tempat_turun = @TempatTurun,
                    nomor = @Nomor
                WHERE id = @Id
            ";

            var result = await conn.ExecuteAsync(sql, new
            {
                surah.Id,
                surah.SurahName,
                surah.ArtiSurat,
                surah.TempatTurun,
                surah.Nomor
            });

            return result > 0;
        }


        // DELETE SURAH
        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = db.Connect();

            string sql = @"
                DELETE FROM surah
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
