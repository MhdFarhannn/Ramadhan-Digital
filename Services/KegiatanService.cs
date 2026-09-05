using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class KegiatanServices
    {
        private readonly Database db;

        public KegiatanServices(Database database)
        {
            db = database;
        }

        // ============================================================
        // GET ALL KEGIATAN
        // ============================================================
        public async Task<IEnumerable<Kegiatan>> GetAllAsync()
        {
            using var conn = db.Connect();

            const string sql = @"
                SELECT
                    id AS Id,
                    judul AS Judul,
                    pemateri AS Pemateri,
                    tanggal AS Tanggal
                FROM kegiatan
                ORDER BY tanggal DESC;
            ";

            return await conn.QueryAsync<Kegiatan>(sql);
        }


        // ============================================================
        // GET KEGIATAN BY ID
        // ============================================================
        public async Task<Kegiatan?> GetByIdAsync(int id)
        {
            using var conn = db.Connect();

            const string sql = @"
                SELECT
                    id AS Id,
                    judul AS Judul,
                    pemateri AS Pemateri,
                    tanggal AS Tanggal
                FROM kegiatan
                WHERE id = @Id;
            ";

            return await conn.QueryFirstOrDefaultAsync<Kegiatan>(
                sql,
                new
                {
                    Id = id
                }
            );
        }


        // ============================================================
        // CREATE KEGIATAN
        // ============================================================
        public async Task<bool> CreateAsync(Kegiatan kegiatan)
        {
            using var conn = db.Connect();

            const string sql = @"
                INSERT INTO kegiatan
                (
                    judul,
                    pemateri,
                    tanggal
                )
                VALUES
                (
                    @Judul,
                    @Pemateri,
                    @Tanggal
                );
            ";

            var result = await conn.ExecuteAsync(
                sql,
                new
                {
                    kegiatan.Judul,
                    kegiatan.Pemateri,
                    kegiatan.Tanggal
                }
            );

            return result > 0;
        }


        // ============================================================
        // REGISTER USER KE KEGIATAN
        //
        // Satu user hanya boleh mendaftar satu kali
        // untuk kegiatan yang sama.
        //
        // Membutuhkan UNIQUE:
        // (id_user, id_kegiatan)
        // ============================================================
        public async Task<bool> RegisterUserAsync(
            KegiatanUser kegiatanUser)
        {
            using var conn = db.Connect();

            const string sql = @"
                INSERT INTO kegiatan_user
                (
                    id_user,
                    id_kegiatan,
                    note
                )
                VALUES
                (
                    @IdUser,
                    @IdKegiatan,
                    @Note
                )
                ON CONFLICT (id_user, id_kegiatan)
                DO NOTHING;
            ";

            var result = await conn.ExecuteAsync(
                sql,
                kegiatanUser
            );

            return result > 0;
        }


        // ============================================================
        // GET KEGIATAN USER
        // ============================================================
        public async Task<IEnumerable<KegiatanUser>> GetByUserIdAsync(
            int idUser)
        {
            using var conn = db.Connect();

            const string sql = @"
                SELECT
                    ku.id AS Id,
                    ku.id_user AS IdUser,
                    ku.id_kegiatan AS IdKegiatan,
                    ku.note AS Note,

                    k.id AS Id,
                    k.judul AS Judul,
                    k.pemateri AS Pemateri,
                    k.tanggal AS Tanggal

                FROM kegiatan_user ku

                INNER JOIN kegiatan k
                    ON ku.id_kegiatan = k.id

                WHERE ku.id_user = @IdUser

                ORDER BY k.tanggal DESC;
            ";

            return await conn.QueryAsync<KegiatanUser, Kegiatan, KegiatanUser>(
                sql,
                (kegiatanUser, kegiatan) =>
                {
                    kegiatanUser.Kegiatan = kegiatan;
                    return kegiatanUser;
                },
                new
                {
                    IdUser = idUser
                },
                splitOn: "Id"
            );
        }


        // ============================================================
        // DELETE KEGIATAN BY ID
        // ============================================================
        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = db.Connect();

            const string sql = @"
                DELETE FROM kegiatan
                WHERE id = @Id;
            ";

            var result = await conn.ExecuteAsync(
                sql,
                new
                {
                    Id = id
                }
            );

            return result > 0;
        }


        // ============================================================
        // UPDATE KEGIATAN
        // ============================================================
        public async Task<bool> UpdateAsync(
            int id,
            Kegiatan kegiatan)
        {
            using var conn = db.Connect();

            const string sql = @"
                UPDATE kegiatan
                SET
                    judul = @Judul,
                    pemateri = @Pemateri,
                    tanggal = @Tanggal
                WHERE id = @Id;
            ";

            var result = await conn.ExecuteAsync(
                sql,
                new
                {
                    Id = id,
                    Judul = kegiatan.Judul,
                    Pemateri = kegiatan.Pemateri,
                    Tanggal = kegiatan.Tanggal
                }
            );

            return result > 0;
        }
    }
}
