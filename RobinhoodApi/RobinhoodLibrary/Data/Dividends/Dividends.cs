using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Dividends
{
    public class Dividends
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Account { get; set; }

        public string Instrument { get; set; }

        public string Amount { get; set; }

        public string Rate { get; set; }

        public string Position { get; set; }

        public string Withholding { get; set; }

        [JsonPropertyName("record_date")]
        public DateTime RecordDate { get; set; }

        [JsonPropertyName("payable_date")]
        public DateTime PayableDate { get; set; }

        [JsonPropertyName("paid_at")]
        public DateTime PaidAt { get; set; }

        public string State { get; set; }

        [JsonPropertyName("drip_enabled")]
        public bool DripEnabled { get; set; }

        [JsonPropertyName("nra_withholding")]
        public string NraWithholding { get; set; }
    }
}
