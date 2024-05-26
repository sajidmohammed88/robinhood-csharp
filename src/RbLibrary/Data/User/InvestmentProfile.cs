namespace Rb.Integration.Api.Data.User;

public class InvestmentProfile
{
	public string User { get; set; }

	public string TotalNetWorth { get; set; }

	public string AnnualIncome { get; set; }

	public string SourceOfFunds { get; set; }

	public string InvestmentObjective { get; set; }

	public string InvestmentExperience { get; set; }

	public string LiquidNetWorth { get; set; }

	public string RiskTolerance { get; set; }

	public string TaxBracket { get; set; }

	public string TimeHorizon { get; set; }

	public string LiquidityNeeds { get; set; }

	public bool InvestmentExperienceCollected { get; set; }

	public bool SuitabilityVerified { get; set; }

	public string OptionTradingExperience { get; set; }

	public bool? ProfessionalTrader { get; set; }

	public bool? UnderstandOptionSpreads { get; set; }

	public string InterestedInOptions { get; set; }

	public DateTime UpdatedAt { get; set; }
}
