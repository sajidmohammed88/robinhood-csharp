using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Positions
{
    public class Position
    {
        public string Url { get; set; }

        public string Instrument { get; set; }

        public string Account { get; set; }

        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("average_buy_price")]
        public string AverageBuyPrice { get; set; }

        [JsonPropertyName("pending_average_buy_price")]
        public string PendingAverageBuyPrice { get; set; }

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; }

        [JsonPropertyName("intraday_average_buy_price")]
        public string IntradayAverageBuyPrice { get; set; }

        [JsonPropertyName("intraday_quantity")]
        public string IntradayQuantity { get; set; }

        [JsonPropertyName("shares_available_for_exercise")]
        public string SharesAvailableForExercise { get; set; }

        [JsonPropertyName("shares_held_for_buys")]
        public string SharesHeldForBuys { get; set; }

        [JsonPropertyName("shares_held_for_sells")]
        public string SharesHeldForSells { get; set; }

        [JsonPropertyName("shares_held_for_stock_grants")]
        public string SharesHeldForStockGrants { get; set; }

        [JsonPropertyName("shares_held_for_options_collateral")]
        public string SharesHeldForOptionsCollateral { get; set; }

        [JsonPropertyName("shares_held_for_options_events")]
        public string SharesHeldForOptionsEvents { get; set; }

        [JsonPropertyName("shares_pending_from_options_events")]
        public string SharesPendingFromOptionsEvents { get; set; }

        [JsonPropertyName("shares_available_for_closing_short_position")]
        public string SharesAvailableForClosingShortPosition { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
