namespace Ramadhan_Digital.Models
{
    public class Surah
    {
        public int Id { get; set; }

        public string SurahName { get; set; }

        public string ArtiSurat { get; set; }

        public string TempatTurun { get; set; }

        public int Nomor { get; set; }


        public ICollection<Ayat> Ayats { get; set; }

        public ICollection<SetoranHafalan> SetoranHafalans { get; set; }
    }

}
