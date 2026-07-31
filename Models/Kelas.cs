namespace Ramadhan_Digital.Models
{
    public class Kelas
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Angkatan { get; set; }

        public ICollection<User> Users { get; set; }
    }

}
