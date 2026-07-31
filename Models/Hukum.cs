namespace Ramadhan_Digital.Models
{
    public class Hukum
    {
        public int Id { get; set; }

        public string Nama { get; set; }


        public ICollection<BacaanSholat> BacaanSholats { get; set; }
    }

}
