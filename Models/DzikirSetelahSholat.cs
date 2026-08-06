using System.Text.Json.Serialization;

namespace Ramadhan_Digital.Models
{
    public class DzikirSetelahSholat
    {
        public int Id { get; set; }

        [JsonPropertyName("nama")]
        public string? Nama { get; set; }

        [JsonPropertyName("arabic")]
        public string? Arabic { get; set; }

        [JsonPropertyName("terjemah")]
        public string? Terjemah { get; set; }

        [JsonPropertyName("sumber")]
        public string? Sumber { get; set; }
    }
}