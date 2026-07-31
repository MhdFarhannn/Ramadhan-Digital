namespace Ramadhan_Digital.Models
{
    public class KategoriSholatWajib
    {
        public int Id { get; set; }

        public string Nama { get; set; }


        public ICollection<DetailSholatWajib> Details { get; set; }
    }

}
