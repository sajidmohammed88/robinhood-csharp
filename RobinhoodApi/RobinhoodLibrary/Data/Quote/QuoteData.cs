using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Quote
{
    public class QuoteData
    {
        [JsonPropertyName("ask_price")]
        public string AskPrice { get; set; }

        [JsonPropertyName("ask_size")]
        public int AskSize { get; set; }

        [JsonPropertyName("bid_price")]
        public string BidPrice { get; set; }

        [JsonPropertyName("bid_size")]
        public int BidSize { get; set; }

        [JsonPropertyName("last_trade_price")]
        public string LastTradePrice { get; set; }

        [JsonPropertyName("last_extended_hours_trade_price")]
        public string LastExtendedHoursTradePrice { get; set; }

        [JsonPropertyName("previous_close")]
        public string PreviousClose { get; set; }

        [JsonPropertyName("adjusted_previous_close")]
        public string AdjustedPreviousClose { get; set; }

        [JsonPropertyName("previous_close_date")]
        public DateTime PreviousCloseDate { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("trading_halted")]
        public bool TradingHalted { get; set; }

        [JsonPropertyName("has_traded")]
        public bool HasTraded { get; set; }

        [JsonPropertyName("last_trade_price_source")]
        public string LastTradePriceSource { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("instrument")]
        public string Instrument { get; set; }

        [JsonPropertyName("instrument_id")]
        public Guid InstrumentId { get; set; }
    }
}
