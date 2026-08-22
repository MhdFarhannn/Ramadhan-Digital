using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    // =========================================================================
    // DTO Ringkas Khusus Response API
    // =========================================================================
    public class IbadahHarianDto
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string? NamaUser { get; set; }
        public DateTime Tanggal { get; set; }
        public bool MembacaAlquran { get; set; }
        public string? TargetBacaan { get; set; }
        public List<DetailSholatDto> DetailSholatWajibs { get; set; } = new();
    }

    public class DetailSholatDto
    {
        public int Id { get; set; }
        public int IdIbadahHarian { get; set; } // Tambahkan ini untuk mapping internal Dapper
        public int IdKategoriSholatWajib { get; set; }
        public string? Kategori { get; set; }
        public int IdStatusSholatWajib { get; set; }
        public string? Status { get; set; }
    }

    public class IbadahHarianServices
    {
        private readonly Database db;

        public IbadahHarianServices(Database database)
        {
            db = database;
        }

        // =========================================================================
        // SECTION 1: METODE SISWA / UMUM
        // =========================================================================

        /// <summary>
        /// Mengambil data ibadah harian milik user tertentu pada tanggal tertentu
        /// </summary>
        public async Task<IbadahHarianDto?> GetByUserAndDateAsync(int idUser, DateTime tanggal)
        {
            using var conn = db.Connect();

            string sqlIbadah = @"
                SELECT 
                    i.id AS Id,
                    i.id_user AS IdUser,
                    u.nama AS NamaUser,
                    i.tanggal::timestamp AS Tanggal,
                    i.membaca_alquran AS MembacaAlquran,
                    i.target_bacaan AS TargetBacaan
                FROM ibadah_harian i
                INNER JOIN users u ON i.id_user = u.id
                WHERE i.id_user = @IdUser AND i.tanggal = @Tanggal;";

            var ibadah = await conn.QueryFirstOrDefaultAsync<IbadahHarianDto>(
                sqlIbadah,
                new { IdUser = idUser, Tanggal = tanggal.Date }
            );

            if (ibadah == null) return null;

            string sqlDetail = @"
                SELECT 
                    ds.id AS Id,
                    ds.id_ibadah_harian AS IdIbadahHarian,
                    ds.id_kategori_sholat_wajib AS IdKategoriSholatWajib,
                    ks.nama AS Kategori,
                    ds.id_status_sholat_wajib AS IdStatusSholatWajib,
                    ss.nama AS Status
                FROM detail_sholat_wajib ds
                LEFT JOIN kategori_sholat_wajib ks ON ds.id_kategori_sholat_wajib = ks.id
                LEFT JOIN status_sholat_wajib ss ON ds.id_status_sholat_wajib = ss.id
                WHERE ds.id_ibadah_harian = @IdIbadahHarian
                ORDER BY ds.id_kategori_sholat_wajib ASC;";

            var details = await conn.QueryAsync<DetailSholatDto>(
                sqlDetail,
                new { IdIbadahHarian = ibadah.Id }
            );

            ibadah.DetailSholatWajibs = details.ToList();
            return ibadah;
        }

        /// <summary>
        /// Menyimpan data ibadah harian. Menolak input jika user sudah mengisi pada tanggal tersebut.
        /// </summary>
        public async Task<(bool Success, string Message)> SaveIbadahHarianAsync(IbadahHarian ibadah)
        {
            using var conn = db.Connect();
            if (conn.State != System.Data.ConnectionState.Open) 
                await conn.OpenAsync();

            using var transaction = conn.BeginTransaction();

            try
            {
                var targetDate = ibadah.Tanggal.Date;

                // 1. Cek apakah data di tanggal tersebut sudah ada
                string checkSql = "SELECT id FROM ibadah_harian WHERE id_user = @IdUser AND tanggal = @Tanggal;";
                var existingId = await conn.QueryFirstOrDefaultAsync<int?>(
                    checkSql,
                    new { ibadah.IdUser, Tanggal = targetDate },
                    transaction
                );

                if (existingId.HasValue)
                {
                    transaction.Rollback();
                    return (false, "Anda sudah mengisi data ibadah harian untuk tanggal ini.");
                }

                // 2. Insert Ibadah Harian Baru
                string insertSql = @"
                    INSERT INTO ibadah_harian (id_user, tanggal, membaca_alquran, target_bacaan)
                    VALUES (@IdUser, @Tanggal, @MembacaAlquran, @TargetBacaan)
                    RETURNING id;";

                int ibadahId = await conn.ExecuteScalarAsync<int>(
                    insertSql,
                    new { ibadah.IdUser, Tanggal = targetDate, ibadah.MembacaAlquran, ibadah.TargetBacaan },
                    transaction
                );

                // 3. Insert Detail Sholat Wajib (Batch Insert)
                if (ibadah.DetailSholatWajibs != null && ibadah.DetailSholatWajibs.Any())
                {
                    string insertDetailSql = @"
                        INSERT INTO detail_sholat_wajib (id_ibadah_harian, id_kategori_sholat_wajib, id_status_sholat_wajib)
                        VALUES (@IdIbadahHarian, @IdKategoriSholatWajib, @IdStatusSholatWajib);";

                    var detailParams = ibadah.DetailSholatWajibs.Select(d => new
                    {
                        IdIbadahHarian = ibadahId,
                        d.IdKategoriSholatWajib,
                        d.IdStatusSholatWajib
                    });

                    await conn.ExecuteAsync(insertDetailSql, detailParams, transaction);
                }

                transaction.Commit();
                return (true, "Data ibadah harian berhasil disimpan.");
            }
            catch (Exception)
            {
                transaction.Rollback();
                return (false, "Terjadi kesalahan saat menyimpan data ibadah harian.");
            }
        }

        // =========================================================================
        // SECTION 2: METODE MONITORING GURU
        // =========================================================================

        /// <summary>
        /// Mengambil daftar seluruh siswa dalam 1 kelas beserta status keterisian ibadahnya
        /// </summary>
        public async Task<IEnumerable<object>> GetMonitoringKelasAsync(int idKelas, DateTime tanggal)
        {
            using var conn = db.Connect();
        
            string sql = @"
                SELECT 
                    u.id AS IdSiswa,
                    u.nama AS NamaLengkap,
                    i.id AS IdIbadah,
                    CASE WHEN i.id IS NOT NULL THEN TRUE ELSE FALSE END AS SudahMengisi,
                    i.membaca_alquran AS MembacaAlquran,
                    i.target_bacaan AS TargetBacaan
                FROM users u
                INNER JOIN role r ON u.id_role = r.id
                LEFT JOIN ibadah_harian i ON u.id = i.id_user AND i.tanggal = @Tanggal
                WHERE u.id_kelas = @IdKelas AND LOWER(r.name) = 'siswa'
                ORDER BY u.nama ASC;";
        
            return await conn.QueryAsync<object>(sql, new { IdKelas = idKelas, Tanggal = tanggal.Date });
        }

        /// <summary>
        /// Mengambil riwayat catatan ibadah milik 1 siswa spesifik (Strongly Typed DTO)
        /// </summary>
        public async Task<IEnumerable<IbadahHarianDto>> GetRiwayatSiswaAsync(int idSiswa, DateTime? startDate, DateTime? endDate)
        {
            using var conn = db.Connect();

            // 1. Query Header Ibadah
            string sqlIbadah = @"
                SELECT 
                    i.id AS Id,
                    i.id_user AS IdUser,
                    u.nama AS NamaUser,
                    i.tanggal::timestamp AS Tanggal,
                    i.membaca_alquran AS MembacaAlquran,
                    i.target_bacaan AS TargetBacaan
                FROM ibadah_harian i
                INNER JOIN users u ON i.id_user = u.id
                WHERE i.id_user = @IdSiswa
                  AND (@StartDate IS NULL OR i.tanggal >= @StartDate)
                  AND (@EndDate IS NULL OR i.tanggal <= @EndDate)
                ORDER BY i.tanggal DESC;";

            var ibadahList = (await conn.QueryAsync<IbadahHarianDto>(sqlIbadah, new 
            { 
                IdSiswa = idSiswa, 
                StartDate = startDate?.Date, 
                EndDate = endDate?.Date 
            })).ToList();

            if (!ibadahList.Any()) return ibadahList;

            // 2. Query Detail Sholat dengan Strongly Typed DTO
            var ibadahIds = ibadahList.Select(i => i.Id).ToList();

            string sqlDetail = @"
                SELECT 
                    ds.id AS Id,
                    ds.id_ibadah_harian AS IdIbadahHarian,
                    ds.id_kategori_sholat_wajib AS IdKategoriSholatWajib,
                    ks.nama AS Kategori,
                    ds.id_status_sholat_wajib AS IdStatusSholatWajib,
                    ss.nama AS Status
                FROM detail_sholat_wajib ds
                LEFT JOIN kategori_sholat_wajib ks ON ds.id_kategori_sholat_wajib = ks.id
                LEFT JOIN status_sholat_wajib ss ON ds.id_status_sholat_wajib = ss.id
                WHERE ds.id_ibadah_harian = ANY(@IbadahIds)
                ORDER BY ds.id_kategori_sholat_wajib ASC;";

            var details = await conn.QueryAsync<DetailSholatDto>(sqlDetail, new { IbadahIds = ibadahIds });

            // 3. Grouping aman secara Type-Safe
            var detailGrouped = details.GroupBy(d => d.IdIbadahHarian).ToDictionary(g => g.Key, g => g.ToList());

            foreach (var ibadah in ibadahList)
            {
                if (detailGrouped.TryGetValue(ibadah.Id, out var itemDetails))
                {
                    ibadah.DetailSholatWajibs = itemDetails;
                }
            }

            return ibadahList;
        }
    }
}