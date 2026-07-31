namespace Ramadhan_Digital.Models
{
    public class KegiatanUser
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdKegiatan { get; set; }

        public string Note { get; set; }


        public User User { get; set; }

        public Kegiatan Kegiatan { get; set; }
    }

}
