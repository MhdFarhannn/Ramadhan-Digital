namespace Ramadhan_Digital.Models
{
    public class BacaanSholat
    {
        public int Id { get; set; }

        public int IdHukum { get; set; }

        public int Urutan { get; set; }

        public string Nama { get; set; }

        public string Gerakan { get; set; }

        public string Arabic { get; set; }

        public string Translate { get; set; }


        public Hukum Hukum { get; set; }
    }

}
