namespace Ramadhan_Digital.Models
{
    public class Kegiatan
    {
        public int Id { get; set; }

        public string Judul { get; set; }

        public string Pemateri { get; set; }

        // kegiatan.tanggal -> PostgreSQL date
        public DateOnly Tanggal { get; set; }


        public ICollection<KegiatanUser> KegiatanUsers { get; set; }
    }

}
