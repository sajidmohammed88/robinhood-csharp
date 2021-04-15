using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Options
{
    public class Chain
    {
        public Guid Id { get; set; }

        public string Symbol { get; set; }

        [JsonPropertyName("can_open_position")]
        public bool CanOpenPosition { get; set; }

        [JsonPropertyName("cash_component")]
        public string CashComponent { get; set; }

        [JsonPropertyName("expiration_dates")]
        public IList<string> ExpirationDates { get; set; }

        [JsonPropertyName("trade_value_multiplier")]
        public string TradeValueMultiplier { get; set; }

        [JsonPropertyName("underlying_instruments")]
        public IList<UnderlyingInstrument> UnderlyingInstruments { get; set; }

        [JsonPropertyName("min_ticks")]
        public MinTicks MinTicks { get; set; }
    }
}
