using Dapper;
using DocumentFormat.OpenXml.Bibliography;
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

        // GET ALL KEGIATAN
        public async Task<IEnumerable<Kegiatan>> GetAllAsync()
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    id AS Id,
                    judul AS Judul,
                    pemateri AS Pemateri,
                    tanggal AS Tanggal
                FROM kegiatan
                ORDER BY tanggal DESC
            ";
            return await conn.QueryAsync<Kegiatan>(sql);
        }

        // GET KEGIATAN BY ID
        public async Task<Kegiatan?> GetByIdAsync(int id)
        {
            using var conn = db.Connect();
            string sql = @"
                SELECT 
                    id AS Id,
                    judul AS Judul,
                    pemateri AS Pemateri,
                    tanggal AS Tanggal
                FROM kegiatan
                WHERE id = @Id
            ";
            return await conn.QueryFirstOrDefaultAsync<Kegiatan>(
                sql,
                new { Id = id }
            );
        }

        // CREATE KEGIATAN
        public async Task<bool> CreateAsync(Kegiatan kegiatan)
        {
            using var conn = db.Connect();
            string sql = @"
                INSERT INTO kegiatan (judul, pemateri, tanggal)
                VALUES (@Judul, @Pemateri, @Tanggal)
            ";
            var result = await conn.ExecuteAsync(sql, kegiatan);
            return result > 0;
        }

        // REGISTER USER KE KEGIATAN
        public async Task<bool> RegisterUserAsync(KegiatanUser kegiatanUser)
        {
            using var conn = db.Connect();
            string sql = @"
                INSERT INTO kegiatan_user (id_user, id_kegiatan, note)
                VALUES (@IdUser, @IdKegiatan, @Note)
            ";
            var result = await conn.ExecuteAsync(sql, kegiatanUser);
            return result > 0;
        }

        // GET KEGIATAN USER BY USER ID 
        public async Task<IEnumerable<KegiatanUser>> GetByUserIdAsync(int idUser)
        {
            using var conn = db.Connect();
            string sql = @"
        SELECT 
            ku.id AS Id,
            ku.id_user AS IdUser,
            ku.id_kegiatan AS IdKegiatan,
            ku.note AS Note,
            k.id AS Id,
            k.judul AS Judul,
            k.pemateri AS Pemateri,
            k.tanggal::timestamp AS Tanggal -- CAST KE TIMESTAMP DI SINI
        FROM kegiatan_user ku
        INNER JOIN kegiatan k ON ku.id_kegiatan = k.id
        WHERE ku.id_user = @IdUser
        ORDER BY k.tanggal DESC
    ";

            return await conn.QueryAsync<KegiatanUser, Kegiatan, KegiatanUser>(
                sql,
                (kegiatanUser, kegiatan) =>
                {
                    kegiatanUser.Kegiatan = kegiatan;
                    return kegiatanUser;
                },
                new { IdUser = idUser },
                splitOn: "Id"
            );
        }
    }
}