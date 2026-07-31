namespace Ramadhan_Digital.Models
{
    public class StatusSetoranHafalan
    {
        public int Id { get; set; }

        public string Nama { get; set; }


        public ICollection<SetoranHafalan> SetoranHafalans { get; set; }
    }

}
