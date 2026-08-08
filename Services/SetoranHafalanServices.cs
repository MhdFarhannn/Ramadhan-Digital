using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class SetoranHafalanServices
    {
        private readonly Database db;

        public SetoranHafalanServices(Database database)
        {
            db = database;
        }

        // GET ALL SETORAN HAFALAN 
        public async Task<IEnumerable<SetoranHafalan>> GetAllAsync()
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    sh.id AS Id,
                    sh.id_user AS IdUser,
                    sh.id_surah AS IdSurah,
                    sh.id_bacaan_sholat AS IdBacaanSholat,
                    sh.id_status_setoran_hafalan AS IdStatusSetoranHafalan,
                    sh.note AS Note,
                    sh.tanggal_setoran::timestamp AS TanggalSetoran,
                    
                    u.id AS Id,
                    
                    s.id AS Id,
                    
                    bs.id AS Id,
                    
                    ssh.id AS Id
                FROM setoran_hafalan sh
                LEFT JOIN users u ON sh.id_user = u.id
                LEFT JOIN surah s ON sh.id_surah = s.id
                LEFT JOIN bacaan_sholat bs ON sh.id_bacaan_sholat = bs.id
                LEFT JOIN status_setoran_hafalan ssh ON sh.id_status_setoran_hafalan = ssh.id
                ORDER BY sh.tanggal_setoran DESC;
            ";

            return await conn.QueryAsync<SetoranHafalan, User, Surah, BacaanSholat, StatusSetoranHafalan, SetoranHafalan>(
                sql,
                (setoran, user, surah, bacaan, status) =>
                {
                    setoran.User = user;
                    setoran.Surah = surah;
                    setoran.BacaanSholat = bacaan;
                    setoran.Status = status;
                    return setoran;
                },
                splitOn: "Id,Id,Id,Id"
            );
        }

        // GET SETORAN HAFALAN BY ID
        public async Task<SetoranHafalan?> GetByIdAsync(int id)
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    sh.id AS Id,
                    sh.id_user AS IdUser,
                    sh.id_surah AS IdSurah,
                    sh.id_bacaan_sholat AS IdBacaanSholat,
                    sh.id_status_setoran_hafalan AS IdStatusSetoranHafalan,
                    sh.note AS Note,
                    sh.tanggal_setoran::timestamp AS TanggalSetoran,

                    u.id AS Id,
                    s.id AS Id,
                    bs.id AS Id,
                    ssh.id AS Id
                FROM setoran_hafalan sh
                LEFT JOIN users u ON sh.id_user = u.id
                LEFT JOIN surah s ON sh.id_surah = s.id
                LEFT JOIN bacaan_sholat bs ON sh.id_bacaan_sholat = bs.id
                LEFT JOIN status_setoran_hafalan ssh ON sh.id_status_setoran_hafalan = ssh.id
                WHERE sh.id = @Id;
            ";

            var result = await conn.QueryAsync<SetoranHafalan, User, Surah, BacaanSholat, StatusSetoranHafalan, SetoranHafalan>(
                sql,
                (setoran, user, surah, bacaan, status) =>
                {
                    setoran.User = user;
                    setoran.Surah = surah;
                    setoran.BacaanSholat = bacaan;
                    setoran.Status = status;
                    return setoran;
                },
                new { Id = id },
                splitOn: "Id,Id,Id,Id"
            );

            return result.FirstOrDefault();
        }

        // GET SETORAN HAFALAN BY USER ID
        public async Task<IEnumerable<SetoranHafalan>> GetByUserIdAsync(int idUser)
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    sh.id AS Id,
                    sh.id_user AS IdUser,
                    sh.id_surah AS IdSurah,
                    sh.id_bacaan_sholat AS IdBacaanSholat,
                    sh.id_status_setoran_hafalan AS IdStatusSetoranHafalan,
                    sh.note AS Note,
                    sh.tanggal_setoran::timestamp AS TanggalSetoran,

                    s.id AS Id,
                    bs.id AS Id,
                    ssh.id AS Id
                FROM setoran_hafalan sh
                LEFT JOIN surah s ON sh.id_surah = s.id
                LEFT JOIN bacaan_sholat bs ON sh.id_bacaan_sholat = bs.id
                LEFT JOIN status_setoran_hafalan ssh ON sh.id_status_setoran_hafalan = ssh.id
                WHERE sh.id_user = @IdUser
                ORDER BY sh.tanggal_setoran DESC;
            ";

            return await conn.QueryAsync<SetoranHafalan, Surah, BacaanSholat, StatusSetoranHafalan, SetoranHafalan>(
                sql,
                (setoran, surah, bacaan, status) =>
                {
                    setoran.Surah = surah;
                    setoran.BacaanSholat = bacaan;
                    setoran.Status = status;
                    return setoran;
                },
                new { IdUser = idUser },
                splitOn: "Id,Id,Id"
            );
        }

        // CREATE SETORAN HAFALAN
        public async Task<bool> CreateAsync(SetoranHafalan setoran)
        {
            using var conn = db.Connect();
            string sql = @"
                INSERT INTO setoran_hafalan (
                    id_user, 
                    id_surah, 
                    id_bacaan_sholat, 
                    id_status_setoran_hafalan, 
                    note, 
                    tanggal_setoran
                )
                VALUES (
                    @IdUser, 
                    @IdSurah, 
                    @IdBacaanSholat, 
                    @IdStatusSetoranHafalan, 
                    @Note, 
                    @TanggalSetoran
                );
            ";
            var result = await conn.ExecuteAsync(sql, setoran);
            return result > 0;
        }

        // UPDATE SETORAN HAFALAN
        public async Task<bool> UpdateAsync(int id, SetoranHafalan setoran)
        {
            using var conn = db.Connect();
            string sql = @"
                UPDATE setoran_hafalan
                SET 
                    id_surah = @IdSurah,
                    id_bacaan_sholat = @IdBacaanSholat,
                    id_status_setoran_hafalan = @IdStatusSetoranHafalan,
                    note = @Note,
                    tanggal_setoran = @TanggalSetoran
                WHERE id = @Id;
            ";

            var result = await conn.ExecuteAsync(sql, new
            {
                Id = id,
                setoran.IdSurah,
                setoran.IdBacaanSholat,
                setoran.IdStatusSetoranHafalan,
                setoran.Note,
                setoran.TanggalSetoran
            });

            return result > 0;
        }

        // DELETE SETORAN HAFALAN
        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = db.Connect();
            string sql = @"DELETE FROM setoran_hafalan WHERE id = @Id;";
            var result = await conn.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}