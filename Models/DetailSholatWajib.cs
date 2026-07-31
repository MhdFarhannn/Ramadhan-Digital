namespace Ramadhan_Digital.Models
{
    public class DetailSholatWajib
    {
        public int Id { get; set; }

        public int IdIbadahHarian { get; set; }

        public int IdKategoriSholatWajib { get; set; }

        public int IdStatusSholatWajib { get; set; }


        public IbadahHarian IbadahHarian { get; set; }

        public KategoriSholatWajib KategoriSholatWajib { get; set; }

        public StatusSholatWajib StatusSholatWajib { get; set; }
    }

}
