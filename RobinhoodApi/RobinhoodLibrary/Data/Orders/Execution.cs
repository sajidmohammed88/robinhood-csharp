using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Orders
{
    public class Execution
    {
        public string Price { get; set; }

        public string Quantity { get; set; }

        [JsonPropertyName("settlement_date")]
        public string SettlementDate { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        public Guid Id { get; set; }

        [JsonPropertyName("ipo_access_execution_rank")]
        public string IpoAccessExecutionRank { get; set; }
    }
}
