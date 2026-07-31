namespace Ramadhan_Digital.Models
{
    public class StatusAbsensi
    {
        public int Id { get; set; }

        public string Nama { get; set; }

        public ICollection<Absensi> Absensis { get; set; }
    }

}
