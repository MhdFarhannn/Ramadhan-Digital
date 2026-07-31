namespace Ramadhan_Digital.Models
{
    public class IbadahHarian
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public DateTime Tanggal { get; set; }

        public bool MembacaAlquran { get; set; }

        public string TargetBacaan { get; set; }


        public User User { get; set; }

        public ICollection<DetailSholatWajib> DetailSholatWajibs { get; set; }
    }

}
