namespace RobinhoodApi.Data.Portfolios;

public class Portfolio
{
	public Uri Url { get; set; }

	public Uri Account { get; set; }

	public DateTime? StartDate { get; set; }

	public string MarketValue { get; set; }

	public string Equity { get; set; }

	public string ExtendedHoursMarketValue { get; set; }

	public string ExtendedHoursEquity { get; set; }

	public string ExtendedHoursPortfolioEquity { get; set; }

	public string LastCoreMarketValue { get; set; }

	public string LastCoreEquity { get; set; }

	public string LastCorePortfolioEquity { get; set; }

	public string ExcessMargin { get; set; }

	public string ExcessMaintenance { get; set; }

	public string ExcessMarginWithUnclearedDeposits { get; set; }

	public string ExcessMaintenanceWithUnclearedDeposits { get; set; }

	public string EquityPreviousClose { get; set; }

	public string PortfolioEquityPreviousClose { get; set; }

	public string AdjustedEquityPreviousClose { get; set; }

	public string AdjustedPortfolioEquityPreviousClose { get; set; }

	public string WithdrawableAmount { get; set; }

	public string UnwithdrawableDeposits { get; set; }

	public string UnwithdrawableGrants { get; set; }
}
