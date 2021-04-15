using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Portfolios
{
    public class Portfolio
    {
        public Uri Url { get; set; }

        public Uri Account { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("market_value")]
        public string MarketValue { get; set; }

        public string Equity { get; set; }

        [JsonPropertyName("extended_hours_market_value")]
        public string ExtendedHoursMarketValue { get; set; }

        [JsonPropertyName("extended_hours_equity")]
        public string ExtendedHoursEquity { get; set; }

        [JsonPropertyName("extended_hours_portfolio_equity")]
        public string ExtendedHoursPortfolioEquity { get; set; }

        [JsonPropertyName("last_core_market_value")]
        public string LastCoreMarketValue { get; set; }

        [JsonPropertyName("last_core_equity")]
        public string LastCoreEquity { get; set; }

        [JsonPropertyName("last_core_portfolio_equity")]
        public string LastCorePortfolioEquity { get; set; }

        [JsonPropertyName("excess_margin")]
        public string ExcessMargin { get; set; }

        [JsonPropertyName("excess_maintenance")]
        public string ExcessMaintenance { get; set; }

        [JsonPropertyName("excess_margin_with_uncleared_deposits")]
        public string ExcessMarginWithUnclearedDeposits { get; set; }

        [JsonPropertyName("excess_maintenance_with_uncleared_deposits")]
        public string ExcessMaintenanceWithUnclearedDeposits { get; set; }

        [JsonPropertyName("equity_previous_close")]
        public string EquityPreviousClose { get; set; }

        [JsonPropertyName("portfolio_equity_previous_close")]
        public string PortfolioEquityPreviousClose { get; set; }

        [JsonPropertyName("adjusted_equity_previous_close")]
        public string AdjustedEquityPreviousClose { get; set; }

        [JsonPropertyName("adjusted_portfolio_equity_previous_close")]
        public string AdjustedPortfolioEquityPreviousClose { get; set; }

        [JsonPropertyName("withdrawable_amount")]
        public string WithdrawableAmount { get; set; }

        [JsonPropertyName("unwithdrawable_deposits")]
        public string UnwithdrawableDeposits { get; set; }

        [JsonPropertyName("unwithdrawable_grants")]
        public string UnwithdrawableGrants { get; set; }
    }
}
