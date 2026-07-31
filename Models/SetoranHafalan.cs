namespace Ramadhan_Digital.Models
{
    public class SetoranHafalan
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdSurah { get; set; }

        public int? IdBacaanSholat { get; set; }

        public int IdStatusSetoranHafalan { get; set; }

        public string Note { get; set; }

        public DateTime TanggalSetoran { get; set; }


        public User User { get; set; }

        public Surah Surah { get; set; }

        public BacaanSholat BacaanSholat { get; set; }

        public StatusSetoranHafalan Status { get; set; }
    }

}
