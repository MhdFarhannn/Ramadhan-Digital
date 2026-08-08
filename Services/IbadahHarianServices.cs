using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class IbadahHarianServices
    {
        private readonly Database db;

        public IbadahHarianServices(Database database)
        {
            db = database;
        }

        // GET IBADAH HARIAN BY USER ID & TANGGAL
        public async Task<IbadahHarian?> GetByUserAndDateAsync(int idUser, DateTime tanggal)
        {
            using var conn = db.Connect();

            string sqlIbadah = @"
                SELECT 
                    id AS Id,
                    id_user AS IdUser,
                    tanggal::timestamp AS Tanggal,
                    membaca_alquran AS MembacaAlquran,
                    target_bacaan AS TargetBacaan
                FROM ibadah_harian
                WHERE id_user = @IdUser AND tanggal = @Tanggal;
            ";

            var ibadah = await conn.QueryFirstOrDefaultAsync<IbadahHarian>(
                sqlIbadah,
                new { IdUser = idUser, Tanggal = tanggal.Date }
            );

            if (ibadah == null) return null;

            string sqlDetail = @"
                SELECT 
                    ds.id AS Id,
                    ds.id_ibadah_harian AS IdIbadahHarian,
                    ds.id_kategori_sholat_wajib AS IdKategoriSholatWajib,
                    ds.id_status_sholat_wajib AS IdStatusSholatWajib,
                    
                    ks.id AS Id,
                    ss.id AS Id
                FROM detail_sholat_wajib ds
                LEFT JOIN kategori_sholat_wajib ks ON ds.id_kategori_sholat_wajib = ks.id
                LEFT JOIN status_sholat_wajib ss ON ds.id_status_sholat_wajib = ss.id
                WHERE ds.id_ibadah_harian = @IdIbadahHarian;
            ";

            var details = await conn.QueryAsync<DetailSholatWajib, KategoriSholatWajib, StatusSholatWajib, DetailSholatWajib>(
                sqlDetail,
                (detail, kategori, status) =>
                {
                    detail.KategoriSholatWajib = kategori;
                    detail.StatusSholatWajib = status;
                    return detail;
                },
                new { IdIbadahHarian = ibadah.Id },
                splitOn: "Id,Id"
            );

            ibadah.DetailSholatWajibs = details.ToList();
            return ibadah;
        }

        // SAVE / UPDATE IBADAH HARIAN + DETAIL SHOLAT
        public async Task<bool> SaveIbadahHarianAsync(IbadahHarian ibadah)
        {
            using var conn = db.Connect();

            using var transaction = conn.BeginTransaction();

            try
            {
                // 1. Cek apakah data di tanggal tersebut sudah ada
                string checkSql = "SELECT id FROM ibadah_harian WHERE id_user = @IdUser AND tanggal = @Tanggal;";
                var existingId = await conn.QueryFirstOrDefaultAsync<int?>(
                    checkSql,
                    new { ibadah.IdUser, Tanggal = ibadah.Tanggal.Date },
                    transaction
                );

                int ibadahId;

                if (existingId.HasValue)
                {
                    ibadahId = existingId.Value;

                    // Update Ibadah Harian
                    string updateSql = @"
                UPDATE ibadah_harian 
                SET membaca_alquran = @MembacaAlquran, 
                    target_bacaan = @TargetBacaan 
                WHERE id = @Id;";

                    await conn.ExecuteAsync(
                        updateSql,
                        new { ibadah.MembacaAlquran, ibadah.TargetBacaan, Id = ibadahId },
                        transaction
                    );

                    // Hapus detail lama untuk diperbarui dengan data baru
                    string deleteDetailSql = "DELETE FROM detail_sholat_wajib WHERE id_ibadah_harian = @IdIbadahHarian;";
                    await conn.ExecuteAsync(deleteDetailSql, new { IdIbadahHarian = ibadahId }, transaction);
                }
                else
                {
                    // Insert Ibadah Harian Baru
                    string insertSql = @"
                INSERT INTO ibadah_harian (id_user, tanggal, membaca_alquran, target_bacaan)
                VALUES (@IdUser, @Tanggal, @MembacaAlquran, @TargetBacaan)
                RETURNING id;";

                    ibadahId = await conn.ExecuteScalarAsync<int>(
                        insertSql,
                        new { ibadah.IdUser, Tanggal = ibadah.Tanggal.Date, ibadah.MembacaAlquran, ibadah.TargetBacaan },
                        transaction
                    );
                }

                // 2. Insert Detail Sholat Wajib
                if (ibadah.DetailSholatWajibs != null && ibadah.DetailSholatWajibs.Any())
                {
                    string insertDetailSql = @"
                INSERT INTO detail_sholat_wajib (id_ibadah_harian, id_kategori_sholat_wajib, id_status_sholat_wajib)
                VALUES (@IdIbadahHarian, @IdKategoriSholatWajib, @IdStatusSholatWajib);";

                    foreach (var detail in ibadah.DetailSholatWajibs)
                    {
                        await conn.ExecuteAsync(insertDetailSql, new
                        {
                            IdIbadahHarian = ibadahId,
                            detail.IdKategoriSholatWajib,
                            detail.IdStatusSholatWajib
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