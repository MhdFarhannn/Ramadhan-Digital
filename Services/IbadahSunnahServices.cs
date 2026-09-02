using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services;

public class IbadahSunnahServices
{
    private readonly Database db;

    public IbadahSunnahServices(Database database)
    {
        db = database;
    }

    // ============================================================
    // GET ALL IBADAH SUNNAH BY USER & TANGGAL
    // ============================================================
    public async Task<IEnumerable<IbadahSunnah>> GetByUserAndDateAsync(
        int idUser,
        DateTime tanggal)
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
            INNER JOIN kategori_sunnah ks
                ON is_s.id_kategori_sunnah = ks.id
            WHERE is_s.id_user = @IdUser
              AND is_s.tanggal = @Tanggal
            ORDER BY ks.id ASC;
        ";

        return await conn.QueryAsync<IbadahSunnah, KategoriSunnah, IbadahSunnah>(
            sql,
            (sunnah, kategori) =>
            {
                sunnah.KategoriSunnah = kategori;
                return sunnah;
            },
            new
            {
                IdUser = idUser,
                Tanggal = tanggal.Date
            },
            splitOn: "Id"
        );
    }


    // ============================================================
    // CEK APAKAH USER SUDAH SUBMIT PADA TANGGAL TERTENTU
    // ============================================================
    public async Task<bool> HasSavedTodayAsync(
        int idUser,
        DateTime tanggal)
    {
        using var conn = db.Connect();

        const string sql = @"
            SELECT EXISTS (
                SELECT 1
                FROM ibadah_sunnah
                WHERE id_user = @IdUser
                  AND tanggal = @Tanggal
            );
        ";

        return await conn.ExecuteScalarAsync<bool>(
            sql,
            new
            {
                IdUser = idUser,
                Tanggal = tanggal.Date
            }
        );
    }


    // ============================================================
    // SAVE IBADAH SUNNAH
    // ============================================================
    public async Task<SaveIbadahResult> SaveIbadahSunnahAsync(
        int idUser,
        DateTime tanggal,
        List<int> idKategoriSunnahList)
    {
        using var conn = db.Connect();

        if (conn.State != System.Data.ConnectionState.Open)
        {
            await conn.OpenAsync();
        }

        using var transaction = await conn.BeginTransactionAsync();

        try
        {
            var tanggalOnly = tanggal.Date;

            
            const string lockSql = @"
                SELECT pg_advisory_xact_lock(
                    hashtext(@LockKey)
                );
            ";

            await conn.ExecuteAsync(
                lockSql,
                new
                {
                    LockKey = $"ibadah-sunnah:{idUser}:{tanggalOnly:yyyy-MM-dd}"
                },
                transaction
            );


            const string checkSql = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM ibadah_sunnah
                    WHERE id_user = @IdUser
                      AND tanggal = @Tanggal
            );

            ";

            var alreadySaved = await conn.ExecuteScalarAsync<bool>(
                checkSql,
                new
                {
                    IdUser = idUser,
                    Tanggal = tanggalOnly
                },
                transaction
            );

            if (alreadySaved)
            {
                await transaction.RollbackAsync();

                return SaveIbadahResult.AlreadySaved;
            }


            // ====================================================
            // INSERT DATA
            // ====================================================
            if (idKategoriSunnahList != null &&
                idKategoriSunnahList.Any())
            {
                const string insertSql = @"
                    INSERT INTO ibadah_sunnah
                    (
                        id_kategori_sunnah,
                        id_user,
                        tanggal
                    )
                    VALUES
                    (
                        @IdKategoriSunnah,
                        @IdUser,
                        @Tanggal
                    );
                ";

                foreach (var idKategori in idKategoriSunnahList.Distinct())
                {
                    await conn.ExecuteAsync(
                        insertSql,
                        new
                        {
                            IdKategoriSunnah = idKategori,
                            IdUser = idUser,
                            Tanggal = tanggalOnly
                        },
                        transaction
                    );
                }
            }


            // ====================================================
            // COMMIT
            // ====================================================
            await transaction.CommitAsync();

            return SaveIbadahResult.Success;
        }
        catch
        {
            await transaction.RollbackAsync();

            return SaveIbadahResult.Failed;
        }
    }


    // ============================================================
    // GET IBADAH SUNNAH MILIK SISWA TERTENTU
    // ============================================================
    public async Task<IEnumerable<IbadahSunnahDto>> GetByUser(
        int idUser,
        DateTime tanggal)
    {
        using var conn = db.Connect();

        const string sql = @"
            SELECT
                id,
                id_kategori_sunnah,
                id_user,
                tanggal::timestamp AS tanggal
            FROM ibadah_sunnah
            WHERE id_user = @IdUser
              AND tanggal = @Tanggal
            ORDER BY id ASC;
        ";

        var result = await conn.QueryAsync<IbadahSunnahDto>(
            sql,
            new
            {
                IdUser = idUser,
                Tanggal = tanggal.Date
            }
        );

        return result;
    }
}


// ================================================================
// HASIL SAVE
// ================================================================
public enum SaveIbadahResult
{
    Success,
    AlreadySaved,
    Failed
}
