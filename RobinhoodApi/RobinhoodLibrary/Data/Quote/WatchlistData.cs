using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Quote
{
    public class WatchlistData
    {
        public string Instrument { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        public string Watchlist { get; set; }

        public string Url { get; set; }
    }
}
