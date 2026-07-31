namespace Ramadhan_Digital.Models
{
    public class Absensi
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public DateTime Tanggal { get; set; }

        public int IdStatusAbsensi { get; set; }


        public User User { get; set; }

        public StatusAbsensi StatusAbsensi { get; set; }
    }

}
