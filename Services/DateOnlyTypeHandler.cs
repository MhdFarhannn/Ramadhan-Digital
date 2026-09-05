using System.Data;
using System.Globalization;

using Dapper;

namespace Ramadhan_Digital.Services
{
    // ================================================================
    // STANDAR TANGGAL PROJECT
    // ================================================================
    //
    // Kolom date-only:
    //
    //     PostgreSQL date  ->  C# DateOnly  ->  JSON "yyyy-MM-dd"
    //
    // Kolom yang benar-benar membutuhkan jam:
    //
    //     PostgreSQL timestamp  ->  C# DateTime  ->  JSON datetime
    //
    // Saat audit dilakukan, seluruh kolom tanggal pada database
    // ramadhan bertipe `date` (tidak ada kolom timestamp),
    // sehingga seluruh property tanggal pada Model/DTO memakai DateOnly.
    //
    //
    // MENGAPA HANDLER INI DIPERLUKAN
    // ----------------------------------------------------------------
    // 1. Npgsql membaca kolom `date` sebagai System.DateOnly.
    //    Bila property C# bertipe DateTime, Dapper akan memanggil
    //    Convert.ChangeType(DateOnly -> DateTime) dan gagal dengan:
    //
    //        System.InvalidCastException:
    //        Object must implement IConvertible.
    //
    //    Solusinya adalah memakai DateOnly pada model,
    //    BUKAN menambahkan cast `tanggal::timestamp` pada SQL.
    //
    // 2. Dapper tidak memiliki dukungan bawaan untuk DateOnly
    //    sebagai parameter:
    //
    //        NotSupportedException:
    //        The member Tanggal of type System.DateOnly
    //        cannot be used as a parameter value
    //
    //    Handler ini yang mengirim DateOnly sebagai PostgreSQL `date`.
    //
    //
    // MENGAPA ITypeHandler, BUKAN SqlMapper.TypeHandler<DateOnly>
    // ----------------------------------------------------------------
    // SqlMapper.TypeHandler<T> tidak memanggil SetValue ketika nilai
    // parameter bernilai NULL, sehingga DbType parameter tidak pernah
    // diisi. Akibatnya pola query berikut gagal untuk DateOnly? null:
    //
    //     WHERE (@StartDate IS NULL OR tanggal >= @StartDate)
    //
    //     42P08: could not determine data type of parameter $1
    //
    // Dengan mengimplementasikan ITypeHandler secara langsung,
    // DbType.Date selalu diisi termasuk untuk nilai NULL.
    //
    //
    // CATATAN PENDAFTARAN
    // ----------------------------------------------------------------
    // Handler ini didaftarkan SATU KALI saja di Program.cs melalui
    // Register(). Dapper otomatis memetakan handler yang sama untuk
    // DateOnly?, jadi tidak perlu handler terpisah untuk tipe nullable
    // dan tidak boleh ada handler DateOnly lain di service manapun.
    // ================================================================

    public sealed class DateOnlyTypeHandler : SqlMapper.ITypeHandler
    {
        private static bool _registered;

        private static readonly object _lock = new();

        /// <summary>
        /// Mendaftarkan handler DateOnly secara global (idempotent).
        /// </summary>
        public static void Register()
        {
            lock (_lock)
            {
                if (_registered)
                {
                    return;
                }

                // Dapper otomatis menambahkan pemetaan untuk DateOnly?
                // ketika handler untuk DateOnly didaftarkan.
                SqlMapper.AddTypeHandler(
                    typeof(DateOnly),
                    new DateOnlyTypeHandler()
                );

                _registered = true;
            }
        }

        // ------------------------------------------------------------
        // DateOnly -> PostgreSQL date
        //
        // Nilai dikirim apa adanya sebagai DateOnly.
        // Tidak ada ToDateTime(), tidak ada komponen jam,
        // tidak ada konversi UTC/local, sehingga tanggal tidak bergeser.
        // ------------------------------------------------------------
        public void SetValue(IDbDataParameter parameter, object? value)
        {
            // Selalu diisi, termasuk saat nilainya NULL,
            // agar PostgreSQL dapat menentukan tipe parameter.
            parameter.DbType = DbType.Date;

            parameter.Value = value switch
            {
                null => DBNull.Value,

                DBNull => DBNull.Value,

                DateOnly dateOnly => dateOnly,

                // Toleransi bila ada pemanggil lama yang masih
                // mengirim DateTime untuk kolom date.
                DateTime dateTime => DateOnly.FromDateTime(dateTime),

                _ => value
            };
        }

        // ------------------------------------------------------------
        // PostgreSQL date -> DateOnly
        // ------------------------------------------------------------
        public object? Parse(Type destinationType, object? value)
        {
            return value switch
            {
                null => null,

                DBNull => null,

                DateOnly dateOnly => dateOnly,

                // Fallback bila query mengembalikan timestamp.
                DateTime dateTime => DateOnly.FromDateTime(dateTime),

                DateTimeOffset dateTimeOffset =>
                    DateOnly.FromDateTime(dateTimeOffset.DateTime),

                string text =>
                    DateOnly.Parse(text, CultureInfo.InvariantCulture),

                _ => throw new DataException(
                    "Tidak dapat memetakan nilai bertipe " +
                    $"'{value.GetType().FullName}' ke " +
                    $"'{destinationType.Name}'. " +
                    "Pastikan kolom database bertipe 'date' " +
                    "dan query tidak melakukan cast ke tipe lain."
                )
            };
        }
    }
}
