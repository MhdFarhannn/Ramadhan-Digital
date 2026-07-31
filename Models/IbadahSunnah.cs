namespace Ramadhan_Digital.Models
{
    public class IbadahSunnah
    {
        public int Id { get; set; }

        public int IdKategoriSunnah { get; set; }

        public int IdUser { get; set; }

        public DateTime Tanggal { get; set; }


        public User User { get; set; }

        public KategoriSunnah KategoriSunnah { get; set; }
    }

}
