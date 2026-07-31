namespace Ramadhan_Digital.Models
{
    public class StatusSholatWajib
    {
        public int Id { get; set; }

        public string Nama { get; set; }


        public ICollection<DetailSholatWajib> Details { get; set; }
    }

}
