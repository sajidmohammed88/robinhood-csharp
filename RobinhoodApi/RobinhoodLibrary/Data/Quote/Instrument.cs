using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Quote
{
    public class Instrument
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Quote { get; set; }

        public string Fundamentals { get; set; }

        public string Splits { get; set; }

        public string State { get; set; }

        public string Market { get; set; }

        [JsonPropertyName("simple_name")]
        public string SimpleName { get; set; }

        public string Name { get; set; }

        public bool Tradeable { get; set; }

        public string Tradability { get; set; }

        public string Symbol { get; set; }

        [JsonPropertyName("bloomberg_unique")]
        public string BloombergUnique { get; set; }

        [JsonPropertyName("margin_initial_ratio")]
        public string MarginInitialRatio { get; set; }

        [JsonPropertyName("maintenance_ratio")]
        public string MaintenanceRatio { get; set; }

        public string Country { get; set; }

        [JsonPropertyName("day_trade_ratio")]
        public string DayTradeRatio { get; set; }

        [JsonPropertyName("list_date")]
        public DateTime ListDate { get; set; }

        [JsonPropertyName("min_tick_size")]
        public string MinTickSize { get; set; }

        public string Type { get; set; }

        [JsonPropertyName("tradable_chain_id")]
        public string TradableChainId { get; set; }

        [JsonPropertyName("rhs_tradability")]
        public string RhsTradability { get; set; }

        [JsonPropertyName("fractional_tradability")]
        public string FractionalTradability { get; set; }

        [JsonPropertyName("default_collar_fraction")]
        public string DefaultCollarFraction { get; set; }

        [JsonPropertyName("ipo_access_status")]
        public string IpoAccessStatus { get; set; }

        [JsonPropertyName("ipo_access_cob_deadline")]
        public string IpoAccessCobDeadline { get; set; }

        [JsonPropertyName("ipo_allocated_price")]
        public string IpoAllocatedPrice { get; set; }

        [JsonPropertyName("ipo_customers_received")]
        public string IpoCustomersReceived { get; set; }

        [JsonPropertyName("ipo_customers_requested")]
        public string IpoCustomersRequested { get; set; }

        [JsonPropertyName("ipo_date")]
        public string IpoDate { get; set; }

        [JsonPropertyName("ipo_s1_url")]
        public string IpoS1Url { get; set; }

        [JsonPropertyName("is_spac")]
        public bool IsSpac { get; set; }

        [JsonPropertyName("is_test")]
        public bool IsTest { get; set; }
    }
}
