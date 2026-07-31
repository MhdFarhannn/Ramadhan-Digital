namespace Ramadhan_Digital.Models
{
    public class Kegiatan
    {
        public int Id { get; set; }

        public string Judul { get; set; }

        public string Pemateri { get; set; }

        public DateTime Tanggal { get; set; }


        public ICollection<KegiatanUser> KegiatanUsers { get; set; }
    }

}
