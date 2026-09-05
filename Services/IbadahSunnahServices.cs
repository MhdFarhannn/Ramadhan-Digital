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
    // GET IBADAH SUNNAH BERDASARKAN USER & TANGGAL
    // ============================================================
    public async Task<IEnumerable<IbadahSunnah>> GetByUserAndDateAsync(
        int idUser,
        DateOnly tanggal)
    {
        using var conn = db.Connect();

        const string sql = @"
            SELECT
                is_s.id AS Id,
                is_s.id_kategori_sunnah AS IdKategoriSunnah,
                is_s.id_user AS IdUser,
                is_s.tanggal AS Tanggal,

                ks.id AS IdKategori,
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
                Tanggal = tanggal
            },
            splitOn: "IdKategori"
        );
    }


    // ============================================================
    // CEK APAKAH USER SUDAH SUBMIT PADA TANGGAL TERTENTU
    // ============================================================
    public async Task<bool> HasSavedTodayAsync(
        int idUser,
        DateOnly tanggal)
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
                Tanggal = tanggal
            }
        );
    }


    // ============================================================
    // SAVE IBADAH SUNNAH
    // ============================================================
    public async Task<SaveIbadahResult> SaveIbadahSunnahAsync(
        int idUser,
        DateOnly tanggal,
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
            // ====================================================
            // LOCK USER + TANGGAL
            // ====================================================
            const string lockSql = @"
                SELECT pg_advisory_xact_lock(
                    hashtext(@LockKey)
                );
            ";

            await conn.ExecuteAsync(
                lockSql,
                new
                {
                    LockKey =
                        $"ibadah-sunnah:{idUser}:{tanggal:yyyy-MM-dd}"
                },
                transaction
            );


            // ====================================================
            // CEK SUDAH PERNAH DISIMPAN
            // ====================================================
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
                    Tanggal = tanggal
                },
                transaction
            );

            if (alreadySaved)
            {
                await transaction.RollbackAsync();

                return SaveIbadahResult.AlreadySaved;
            }


            // ====================================================
            // INSERT IBADAH SUNNAH
            // ====================================================
            if (idKategoriSunnahList != null &&
                idKategoriSunnahList.Count > 0)
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

                foreach (var idKategori in
                    idKategoriSunnahList.Distinct())
                {
                    await conn.ExecuteAsync(
                        insertSql,
                        new
                        {
                            IdKategoriSunnah = idKategori,
                            IdUser = idUser,
                            Tanggal = tanggal
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
    // GET MONITORING IBADAH SUNNAH SISWA
    // PADA TANGGAL TERTENTU
    //
    // Semua kategori sunnah ditampilkan.
    //
    // Sudah melakukan:
    //     SudahDilakukan = true
    //
    // Belum melakukan:
    //     SudahDilakukan = false
    // ============================================================
    public async Task<IEnumerable<IbadahSunnahMonitoringDto>> GetByUser(
        int idUser,
        DateOnly tanggal)
    {
        using var conn = db.Connect();

        const string sql = @"
            SELECT
                ks.id AS IdKategoriSunnah,
                ks.nama AS Nama,

                CASE
                    WHEN is_s.id IS NOT NULL
                    THEN TRUE
                    ELSE FALSE
                END AS SudahDilakukan,

                is_s.id AS IdIbadahSunnah,
                is_s.id_user AS IdUser,
                is_s.tanggal AS Tanggal

            FROM kategori_sunnah ks

            LEFT JOIN ibadah_sunnah is_s
                ON is_s.id_kategori_sunnah = ks.id
                AND is_s.id_user = @IdUser
                AND is_s.tanggal = @Tanggal

            ORDER BY ks.id ASC;
        ";

        return await conn.QueryAsync<IbadahSunnahMonitoringDto>(
            sql,
            new
            {
                IdUser = idUser,
                Tanggal = tanggal
            }
        );
    }


    // ============================================================
    // GET SELURUH RIWAYAT IBADAH SUNNAH SISWA
    //
    // Tidak menggunakan filter tanggal.
    //
    // Mengembalikan semua ibadah sunnah yang pernah
    // dilakukan oleh siswa.
    // ============================================================
    public async Task<IEnumerable<IbadahSunnahDto>> GetByUserAsync(
        int idUser)
    {
        using var conn = db.Connect();

        const string sql = @"
            SELECT
                is_s.id AS Id,
                is_s.id_kategori_sunnah AS IdKategoriSunnah,
                is_s.id_user AS IdUser,
                is_s.tanggal AS Tanggal
            FROM ibadah_sunnah is_s
            WHERE is_s.id_user = @IdUser
            ORDER BY
                is_s.tanggal DESC,
                is_s.id ASC;
        ";

        return await conn.QueryAsync<IbadahSunnahDto>(
            sql,
            new
            {
                IdUser = idUser
            }
        );
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