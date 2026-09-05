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

        // ============================================================
        // GET ALL SETORAN SURAH
        // ============================================================
        public async Task<IEnumerable<SetoranHafalan>> GetAllSurahAsync()
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
                    sh.tanggal_setoran AS TanggalSetoran,

                    u.id AS UserId,
                    u.*,

                    s.id AS SurahId,
                    s.*,

                    ssh.id AS StatusId,
                    ssh.*
                    
                FROM setoran_hafalan sh

                LEFT JOIN users u 
                    ON sh.id_user = u.id

                LEFT JOIN surah s 
                    ON sh.id_surah = s.id

                LEFT JOIN status_setoran_hafalan ssh 
                    ON sh.id_status_setoran_hafalan = ssh.id

                WHERE sh.id_surah IS NOT NULL
                  AND sh.id_bacaan_sholat IS NULL

                ORDER BY sh.tanggal_setoran DESC;
            ";

            return await conn.QueryAsync<
                SetoranHafalan,
                User,
                Surah,
                StatusSetoranHafalan,
                SetoranHafalan
            >(
                sql,
                (setoran, user, surah, status) =>
                {
                    setoran.User = user;
                    setoran.Surah = surah;
                    setoran.Status = status;

                    return setoran;
                },
                splitOn: "UserId,SurahId,StatusId"
            );
        }


        // ============================================================
        // GET ALL SETORAN BACAAN SHOLAT
        // ============================================================
        public async Task<IEnumerable<SetoranHafalan>> GetAllBacaanSholatAsync()
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
                    sh.tanggal_setoran AS TanggalSetoran,

                    u.id AS UserId,
                    u.*,

                    bs.id AS BacaanSholatId,
                    bs.*,

                    ssh.id AS StatusId,
                    ssh.*
                    
                FROM setoran_hafalan sh

                LEFT JOIN users u 
                    ON sh.id_user = u.id

                LEFT JOIN bacaan_sholat bs 
                    ON sh.id_bacaan_sholat = bs.id

                LEFT JOIN status_setoran_hafalan ssh 
                    ON sh.id_status_setoran_hafalan = ssh.id

                WHERE sh.id_bacaan_sholat IS NOT NULL
                  AND sh.id_surah IS NULL

                ORDER BY sh.tanggal_setoran DESC;
            ";

            return await conn.QueryAsync<
                SetoranHafalan,
                User,
                BacaanSholat,
                StatusSetoranHafalan,
                SetoranHafalan
            >(
                sql,
                (setoran, user, bacaan, status) =>
                {
                    setoran.User = user;
                    setoran.BacaanSholat = bacaan;
                    setoran.Status = status;

                    return setoran;
                },
                splitOn: "UserId,BacaanSholatId,StatusId"
            );
        }


        // ============================================================
        // GET SETORAN SURAH BY ID
        // ============================================================
        public async Task<SetoranHafalan?> GetSurahByIdAsync(int id)
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
                    sh.tanggal_setoran AS TanggalSetoran,

                    u.id AS UserId,
                    u.*,

                    s.id AS SurahId,
                    s.*,

                    ssh.id AS StatusId,
                    ssh.*

                FROM setoran_hafalan sh

                LEFT JOIN users u 
                    ON sh.id_user = u.id

                LEFT JOIN surah s 
                    ON sh.id_surah = s.id

                LEFT JOIN status_setoran_hafalan ssh 
                    ON sh.id_status_setoran_hafalan = ssh.id

                WHERE sh.id = @Id
                  AND sh.id_surah IS NOT NULL
                  AND sh.id_bacaan_sholat IS NULL;
            ";

            var result = await conn.QueryAsync<
                SetoranHafalan,
                User,
                Surah,
                StatusSetoranHafalan,
                SetoranHafalan
            >(
                sql,
                (setoran, user, surah, status) =>
                {
                    setoran.User = user;
                    setoran.Surah = surah;
                    setoran.Status = status;

                    return setoran;
                },
                new { Id = id },
                splitOn: "UserId,SurahId,StatusId"
            );

            return result.FirstOrDefault();
        }


        // ============================================================
        // GET SETORAN BACAAN SHOLAT BY ID
        // ============================================================
        public async Task<SetoranHafalan?> GetBacaanSholatByIdAsync(int id)
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
                    sh.tanggal_setoran AS TanggalSetoran,

                    u.id AS UserId,
                    u.*,

                    bs.id AS BacaanSholatId,
                    bs.*,

                    ssh.id AS StatusId,
                    ssh.*

                FROM setoran_hafalan sh

                LEFT JOIN users u 
                    ON sh.id_user = u.id

                LEFT JOIN bacaan_sholat bs 
                    ON sh.id_bacaan_sholat = bs.id

                LEFT JOIN status_setoran_hafalan ssh 
                    ON sh.id_status_setoran_hafalan = ssh.id

                WHERE sh.id = @Id
                  AND sh.id_bacaan_sholat IS NOT NULL
                  AND sh.id_surah IS NULL;
            ";

            var result = await conn.QueryAsync<
                SetoranHafalan,
                User,
                BacaanSholat,
                StatusSetoranHafalan,
                SetoranHafalan
            >(
                sql,
                (setoran, user, bacaan, status) =>
                {
                    setoran.User = user;
                    setoran.BacaanSholat = bacaan;
                    setoran.Status = status;

                    return setoran;
                },
                new { Id = id },
                splitOn: "UserId,BacaanSholatId,StatusId"
            );

            return result.FirstOrDefault();
        }


        // ============================================================
        // GET SETORAN SURAH BY USER
        // ============================================================
        public async Task<IEnumerable<SetoranHafalan>> GetSurahByUserIdAsync(int idUser)
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
                    sh.tanggal_setoran AS TanggalSetoran,

                    s.id AS SurahId,
                    s.*,

                    ssh.id AS StatusId,
                    ssh.*

                FROM setoran_hafalan sh

                LEFT JOIN surah s 
                    ON sh.id_surah = s.id

                LEFT JOIN status_setoran_hafalan ssh 
                    ON sh.id_status_setoran_hafalan = ssh.id

                WHERE sh.id_user = @IdUser
                  AND sh.id_surah IS NOT NULL
                  AND sh.id_bacaan_sholat IS NULL

                ORDER BY sh.tanggal_setoran DESC;
            ";

            return await conn.QueryAsync<
                SetoranHafalan,
                Surah,
                StatusSetoranHafalan,
                SetoranHafalan
            >(
                sql,
                (setoran, surah, status) =>
                {
                    setoran.Surah = surah;
                    setoran.Status = status;

                    return setoran;
                },
                new { IdUser = idUser },
                splitOn: "SurahId,StatusId"
            );
        }


        // ============================================================
        // GET SETORAN BACAAN SHOLAT BY USER
        // ============================================================
        public async Task<IEnumerable<SetoranHafalan>> GetBacaanSholatByUserIdAsync(int idUser)
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
                    sh.tanggal_setoran AS TanggalSetoran,

                    bs.id AS BacaanSholatId,
                    bs.*,

                    ssh.id AS StatusId,
                    ssh.*

                FROM setoran_hafalan sh

                LEFT JOIN bacaan_sholat bs 
                    ON sh.id_bacaan_sholat = bs.id

                LEFT JOIN status_setoran_hafalan ssh 
                    ON sh.id_status_setoran_hafalan = ssh.id

                WHERE sh.id_user = @IdUser
                  AND sh.id_bacaan_sholat IS NOT NULL
                  AND sh.id_surah IS NULL

                ORDER BY sh.tanggal_setoran DESC;
            ";

            return await conn.QueryAsync<
                SetoranHafalan,
                BacaanSholat,
                StatusSetoranHafalan,
                SetoranHafalan
            >(
                sql,
                (setoran, bacaan, status) =>
                {
                    setoran.BacaanSholat = bacaan;
                    setoran.Status = status;

                    return setoran;
                },
                new { IdUser = idUser },
                splitOn: "BacaanSholatId,StatusId"
            );
        }


        // ============================================================
        // CREATE SETORAN SURAH
        // HANYA BOLEH 1x UNTUK SETIAP SURAH PER USER
        // ============================================================
        public async Task<(bool Success, string Message)> CreateSurahAsync(
            SetoranHafalan setoran)
        {
            using var conn = db.Connect();
        
            // Cek apakah user sudah pernah setor surah ini
            string checkSql = @"
                SELECT COUNT(1)
                FROM setoran_hafalan
                WHERE id_user = @IdUser
                  AND id_surah = @IdSurah;
            ";
        
            var existing = await conn.ExecuteScalarAsync<int>(
                checkSql,
                new
                {
                    setoran.IdUser,
                    setoran.IdSurah
                });
        
            if (existing > 0)
            {
                return (
                    false,
                    "Surah tersebut sudah pernah disetorkan oleh user ini."
                );
            }
        
            // Insert
            string insertSql = @"
                INSERT INTO setoran_hafalan
                (
                    id_user,
                    id_surah,
                    id_bacaan_sholat,
                    id_status_setoran_hafalan,
                    note,
                    tanggal_setoran
                )
                VALUES
                (
                    @IdUser,
                    @IdSurah,
                    NULL,
                    @IdStatusSetoranHafalan,
                    @Note,
                    @TanggalSetoran
                );
            ";
        
            var result = await conn.ExecuteAsync(
                insertSql,
                setoran
            );
        
            if (result <= 0)
            {
                return (
                    false,
                    "Gagal menambahkan setoran surah."
                );
            }
        
            return (
                true,
                "Setoran surah berhasil ditambahkan."
            );
        }

        // ============================================================
        // CREATE SETORAN BACAAN SHOLAT
        // ============================================================
        public async Task<bool> CreateBacaanSholatAsync(SetoranHafalan setoran)
        {
            using var conn = db.Connect();

            string sql = @"
                INSERT INTO setoran_hafalan
                (
                    id_user,
                    id_surah,
                    id_bacaan_sholat,
                    id_status_setoran_hafalan,
                    note,
                    tanggal_setoran
                )
                VALUES
                (
                    @IdUser,
                    NULL,
                    @IdBacaanSholat,
                    @IdStatusSetoranHafalan,
                    @Note,
                    @TanggalSetoran
                );
            ";

            var result = await conn.ExecuteAsync(sql, setoran);

            return result > 0;
        }


        // ============================================================
        // UPDATE SETORAN SURAH
        // ============================================================
        public async Task<bool> UpdateSurahAsync(
            int id,
            SetoranHafalan setoran)
        {
            using var conn = db.Connect();

            string sql = @"
                UPDATE setoran_hafalan
                SET
                    id_surah = @IdSurah,
                    id_bacaan_sholat = NULL,
                    id_status_setoran_hafalan = @IdStatusSetoranHafalan,
                    note = @Note,
                    tanggal_setoran = @TanggalSetoran
                WHERE id = @Id
                  AND id_surah IS NOT NULL
                  AND id_bacaan_sholat IS NULL;
            ";

            var result = await conn.ExecuteAsync(sql, new
            {
                Id = id,
                setoran.IdSurah,
                setoran.IdStatusSetoranHafalan,
                setoran.Note,
                setoran.TanggalSetoran
            });

            return result > 0;
        }


        // ============================================================
        // UPDATE SETORAN BACAAN SHOLAT
        // ============================================================
        public async Task<bool> UpdateBacaanSholatAsync(
            int id,
            SetoranHafalan setoran)
        {
            using var conn = db.Connect();

            string sql = @"
                UPDATE setoran_hafalan
                SET
                    id_surah = NULL,
                    id_bacaan_sholat = @IdBacaanSholat,
                    id_status_setoran_hafalan = @IdStatusSetoranHafalan,
                    note = @Note,
                    tanggal_setoran = @TanggalSetoran
                WHERE id = @Id
                  AND id_bacaan_sholat IS NOT NULL
                  AND id_surah IS NULL;
            ";

            var result = await conn.ExecuteAsync(sql, new
            {
                Id = id,
                setoran.IdBacaanSholat,
                setoran.IdStatusSetoranHafalan,
                setoran.Note,
                setoran.TanggalSetoran
            });

            return result > 0;
        }


        // ============================================================
        // DELETE SETORAN SURAH
        // ============================================================
        public async Task<bool> DeleteSurahAsync(int id)
        {
            using var conn = db.Connect();

            string sql = @"
                DELETE FROM setoran_hafalan
                WHERE id = @Id
                  AND id_surah IS NOT NULL
                  AND id_bacaan_sholat IS NULL;
            ";

            var result = await conn.ExecuteAsync(
                sql,
                new { Id = id }
            );

            return result > 0;
        }


        // ============================================================
        // DELETE SETORAN BACAAN SHOLAT
        // ============================================================
        public async Task<bool> DeleteBacaanSholatAsync(int id)
        {
            using var conn = db.Connect();

            string sql = @"
                DELETE FROM setoran_hafalan
                WHERE id = @Id
                  AND id_bacaan_sholat IS NOT NULL
                  AND id_surah IS NULL;
            ";

            var result = await conn.ExecuteAsync(
                sql,
                new { Id = id }
            );

            return result > 0;
        }

        // ============================================================
        // GET SETORAN SURAH BY USER ID
        // ============================================================
    }
}
