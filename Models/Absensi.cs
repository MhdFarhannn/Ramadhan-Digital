namespace Ramadhan_Digital.Models
{
    public class Absensi
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        // absensi.tanggal -> PostgreSQL date
        public DateOnly Tanggal { get; set; }

        public int IdStatusAbsensi { get; set; }

        public User User { get; set; }

        public StatusAbsensi StatusAbsensi { get; set; }
    }
}
