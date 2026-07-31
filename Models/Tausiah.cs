namespace Ramadhan_Digital.Models
{
    public class Tausiah
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public DateTime Tanggal { get; set; }

        public string JudulTausiah { get; set; }

        public string NamaPenceramah { get; set; }

        public string Ringkasan { get; set; }


        public User User { get; set; }
    }

}
