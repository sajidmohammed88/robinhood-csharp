using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Quote
{
    public class HistoricalsData
    {
        public string Quote { get; set; }

        public string Symbol { get; set; }

        public string Interval { get; set; }

        public string Span { get; set; }

        public string Bounds { get; set; }

        [JsonPropertyName("previous_close_price")]
        public string PreviousClosePrice { get; set; }

        [JsonPropertyName("previous_close_time")]
        public DateTime PreviousCloseTime { get; set; }

        [JsonPropertyName("open_price")]
        public string OpenPrice { get; set; }

        [JsonPropertyName("open_time")]
        public DateTime OpenTime { get; set; }

        public string Instrument { get; set; }

        [JsonPropertyName("InstrumentID")]
        public string InstrumentId { get; set; }

        public IList<Historical> Historicals { get; set; }
    }
}
