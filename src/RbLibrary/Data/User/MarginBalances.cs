namespace Rb.Integration.Api.Data.User;

public class MarginBalances
{
	public string UnclearedDeposits { get; set; }

	public string Cash { get; set; }

	public string CashHeldForDividends { get; set; }

	public string CashHeldForRestrictions { get; set; }

	public string CashHeldForNummusRestrictions { get; set; }

	public string CashHeldForOrders { get; set; }

	public string CashAvailableForWithdrawal { get; set; }

	public string UnsettledFunds { get; set; }

	public string UnsettledDebit { get; set; }

	public string OutstandingInterest { get; set; }

	public string UnallocatedMarginCash { get; set; }

	public string MarginLimit { get; set; }

	public string CryptoBuyingPower { get; set; }

	public string DayTradeBuyingPower { get; set; }

	public string Sma { get; set; }

	public bool? DayTradesProtection { get; set; }

	public string StartOfDayOvernightBuyingPower { get; set; }

	public string OvernightBuyingPower { get; set; }

	public string OvernightBuyingPowerHeldForOrders { get; set; }

	public string DayTradeBuyingPowerHeldForOrders { get; set; }

	public string OvernightRatio { get; set; }

	public string DayTradeRatio { get; set; }

	public string MarkedPatternDayTraderDate { get; set; }

	public DateTime? CreatedAt { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public string StartOfDayDtbp { get; set; }

	public string PortfolioCash { get; set; }

	public string CashHeldForOptionsCollateral { get; set; }

	public string GoldEquityRequirement { get; set; }

	public string UnclearedNummusDeposits { get; set; }

	public string CashPendingFromOptionsEvents { get; set; }

	public string SettledAmountBorrowed { get; set; }

	public string PendingDeposit { get; set; }

	public string FundingHoldBalance { get; set; }

	public string PendingDebitCardDebits { get; set; }

	public string NetMovingCash { get; set; }

	public string MarginWithdrawalLimit { get; set; }

	public string InstantUsed { get; set; }

	public string InstantAllocated { get; set; }

	public string EligibleDepositAsInstant { get; set; }

	public bool? LeverageEnabled { get; set; }
}
