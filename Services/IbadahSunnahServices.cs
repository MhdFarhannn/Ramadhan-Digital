using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class IbadahSunnahServices
    {
        private readonly Database db;

        public IbadahSunnahServices(Database database)
        {
            db = database;
        }

        // GET ALL IBADAH SUNNAH BY USER & TANGGAL (Multi-Mapping)
        public async Task<IEnumerable<IbadahSunnah>> GetByUserAndDateAsync(int idUser, DateTime tanggal)
        {
            using var conn = db.Connect();

            string sql = @"
        SELECT 
            is_s.id AS Id,
            is_s.id_kategori_sunnah AS IdKategoriSunnah,
            is_s.id_user AS IdUser,
            is_s.tanggal::timestamp AS Tanggal,

            ks.id AS Id,
            ks.nama AS Nama
        FROM ibadah_sunnah is_s
        INNER JOIN kategori_sunnah ks ON is_s.id_kategori_sunnah = ks.id
        WHERE is_s.id_user = @IdUser AND is_s.tanggal = @Tanggal
        ORDER BY ks.id ASC;
    ";

            return await conn.QueryAsync<IbadahSunnah, KategoriSunnah, IbadahSunnah>(
                sql,
                (sunnah, kategori) =>
                {
                    sunnah.KategoriSunnah = kategori;
                    return sunnah;
                },
                new { IdUser = idUser, Tanggal = tanggal.Date },
                splitOn: "Id"
            );
        }


        // SAVE / UPDATE IBADAH SUNNAH (REPLACE LIST SUNNAH HARI INI)
        public async Task<bool> SaveIbadahSunnahAsync(int idUser, DateTime tanggal, List<int> idKategoriSunnahList)
        {
            using var conn = db.Connect();
            using var transaction = conn.BeginTransaction();

            try
            {
                // 1. Hapus data ibadah sunnah user pada tanggal tersebut terlebih dahulu (supaya tidak duplikat saat update)
                string deleteSql = "DELETE FROM ibadah_sunnah WHERE id_user = @IdUser AND tanggal = @Tanggal;";
                await conn.ExecuteAsync(deleteSql, new { IdUser = idUser, Tanggal = tanggal.Date }, transaction);

                // 2. Insert ulang daftar ibadah sunnah yang dicentang
                if (idKategoriSunnahList != null && idKategoriSunnahList.Any())
                {
                    string insertSql = @"
                        INSERT INTO ibadah_sunnah (id_kategori_sunnah, id_user, tanggal)
                        VALUES (@IdKategoriSunnah, @IdUser, @Tanggal);
                    ";

                    foreach (var idKategori in idKategoriSunnahList)
                    {
                        await conn.ExecuteAsync(insertSql, new
                        {
                            IdKategoriSunnah = idKategori,
                            IdUser = idUser,
                            Tanggal = tanggal.Date
                        }, transaction);
                    }
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}