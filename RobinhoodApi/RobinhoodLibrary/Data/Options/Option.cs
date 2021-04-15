using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Options
{
    public class Option
    {
        public string Account { get; set; }

        [JsonPropertyName("average_price")]
        public string AveragePrice { get; set; }

        [JsonPropertyName("chain_id")]
        public string ChainId { get; set; }

        [JsonPropertyName("chain_symbol")]
        public string ChainSymbol { get; set; }

        public Guid Id { get; set; }

        [JsonPropertyName("option")]
        public string OptionUrl { get; set; }

        public string Type { get; set; }

        [JsonPropertyName("pending_buy_quantity")]
        public string PendingBuyQuantity { get; set; }

        [JsonPropertyName("pending_expired_quantity")]
        public string PendingExpiredQuantity { get; set; }

        [JsonPropertyName("pending_expiration_quantity")]
        public string PendingExpirationQuantity { get; set; }

        [JsonPropertyName("pending_exercise_quantity")]
        public string PendingExerciseQuantity { get; set; }

        [JsonPropertyName("pending_assignment_quantity")]
        public string PendingAssignmentQuantity { get; set; }

        [JsonPropertyName("pending_sell_quantity")]
        public string PendingSellQuantity { get; set; }

        public string Quantity { get; set; }

        [JsonPropertyName("intraday_quantity")]
        public string IntradayQuantity { get; set; }

        [JsonPropertyName("intraday_average_open_price")]
        public string IntradayAverageOpenPrice { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("trade_value_multiplier")]
        public string TradeValueMultiplier { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public string Url { get; set; }

        [JsonPropertyName("option_id")]
        public string OptionId { get; set; }
    }
}
