namespace Ramadhan_Digital.Models
{
    public class User
    {
        public int Id { get; set; }

        public int IdRole { get; set; }
        public int? IdKelas { get; set; }

        public string Nama { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        public Role Role { get; set; }
        public Kelas Kelas { get; set; }


        public ICollection<Absensi> Absensis { get; set; }

        public ICollection<SetoranHafalan> SetoranHafalans { get; set; }

        public ICollection<IbadahHarian> IbadahHarians { get; set; }

        public ICollection<IbadahSunnah> IbadahSunnahs { get; set; }

        public ICollection<KegiatanUser> KegiatanUsers { get; set; }

        public ICollection<Tausiah> Tausiahs { get; set; }
    }

}
