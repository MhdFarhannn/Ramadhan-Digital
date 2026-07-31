namespace Ramadhan_Digital.Models
{
    public class Ayat
    {
        public int Id { get; set; }

        public int IdSurah { get; set; }

        public int Nomor { get; set; }

        public string Arab { get; set; }

        public string Terjemah { get; set; }


        public Surah Surah { get; set; }
    }

}
