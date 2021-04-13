using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Quote
{
    public class Historical
    {
        [JsonPropertyName("begins_at")]
        public DateTime BeginsAt { get; set; }

        [JsonPropertyName("open_price")]
        public string OpenPrice { get; set; }

        [JsonPropertyName("close_price")]
        public string ClosePrice { get; set; }

        [JsonPropertyName("high_price")]
        public string HighPrice { get; set; }

        [JsonPropertyName("low_price")]
        public string LowPrice { get; set; }

        public int Volume { get; set; }

        public string Session { get; set; }

        public bool Interpolated { get; set; }
    }
}
