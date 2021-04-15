using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.User
{
    public class InvestmentProfile
    {
        public string User { get; set; }

        [JsonPropertyName("total_net_worth")]
        public string TotalNetWorth { get; set; }

        [JsonPropertyName("annual_income")]
        public string AnnualIncome { get; set; }

        [JsonPropertyName("source_of_funds")]
        public string SourceOfFunds { get; set; }

        [JsonPropertyName("investment_objective")]
        public string InvestmentObjective { get; set; }

        [JsonPropertyName("investment_experience")]
        public string InvestmentExperience { get; set; }

        [JsonPropertyName("liquid_net_worth")]
        public string LiquidNetWorth { get; set; }

        [JsonPropertyName("risk_tolerance")]
        public string RiskTolerance { get; set; }

        [JsonPropertyName("tax_bracket")]
        public string TaxBracket { get; set; }

        [JsonPropertyName("time_horizon")]
        public string TimeHorizon { get; set; }

        [JsonPropertyName("liquidity_needs")]
        public string LiquidityNeeds { get; set; }

        [JsonPropertyName("investment_experience_collected")]
        public bool InvestmentExperienceCollected { get; set; }

        [JsonPropertyName("suitability_verified")]
        public bool SuitabilityVerified { get; set; }

        [JsonPropertyName("option_trading_experience")]
        public string OptionTradingExperience { get; set; }

        [JsonPropertyName("professional_trader")]
        public bool? ProfessionalTrader { get; set; }

        [JsonPropertyName("understand_option_spreads")]
        public bool? UnderstandOptionSpreads { get; set; }

        [JsonPropertyName("interested_in_options")]
        public string InterestedInOptions { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
