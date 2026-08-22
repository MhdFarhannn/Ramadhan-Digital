using System.Text.Json.Serialization;

namespace Ramadhan_Digital.Models
{
    public class Ayat
    {
        public int Id { get; set; }

        [JsonPropertyName("idSurah")]
        public int IdSurah { get; set; }

        public int Nomor { get; set; }

        [JsonPropertyName("arab")]
        public string? Arab { get; set; }

        [JsonPropertyName("terjemah")]
        public string? Terjemah { get; set; }

        [JsonIgnore]
        public Surah? Surah { get; set; }
    }
}



