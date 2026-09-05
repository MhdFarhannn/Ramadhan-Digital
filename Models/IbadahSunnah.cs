namespace Ramadhan_Digital.Models
{
    public class IbadahSunnah
    {
        public int Id { get; set; }

        public int IdKategoriSunnah { get; set; }

        public int IdUser { get; set; }

        // ibadah_sunnah.tanggal -> PostgreSQL date
        public DateOnly Tanggal { get; set; }


        public User User { get; set; }

        public KategoriSunnah KategoriSunnah { get; set; }
    }

    public class IbadahSunnahDto
    {
        public int Id { get; set; }
        public int IdKategoriSunnah { get; set; }
        public int IdUser { get; set; }
        public DateOnly Tanggal { get; set; }
       
    }

    
    
    public class IbadahSunnahMonitoringDto
    {
        public int IdKategoriSunnah { get; set; }
    
        public string Nama { get; set; } = string.Empty;
    
        public bool SudahDilakukan { get; set; }
    
        public int? IdIbadahSunnah { get; set; }
    
        public int? IdUser { get; set; }
    
        public DateOnly? Tanggal { get; set; }
    }

    
    public class SaveIbadahSunnahRequest
    {
        public DateOnly Tanggal { get; set; }
    
        public List<int> IdKategoriSunnahList { get; set; } = new();
    }
}
