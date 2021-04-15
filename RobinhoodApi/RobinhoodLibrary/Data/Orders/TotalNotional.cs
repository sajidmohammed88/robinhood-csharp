using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Orders
{
    public class TotalNotional
    {
        public string Amount { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("currency_id")]
        public Guid CurrencyId { get; set; }
    }
}
