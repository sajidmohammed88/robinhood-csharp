namespace RobinhoodApi.Data.User;

public class Account
{
	public string Url { get; set; }

	public string PortfolioCash { get; set; }

	public string CanDowngradeToCash { get; set; }

	public string User { get; set; }

	public string AccountNumber { get; set; }

	public string Type { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public bool Deactivated { get; set; }

	public bool DepositHalted { get; set; }

	public bool WithdrawalHalted { get; set; }

	public bool OnlyPositionClosingTrades { get; set; }

	public string BuyingPower { get; set; }

	public string CashAvailableForWithdrawal { get; set; }

	public string Cash { get; set; }

	public string AmountEligibleForDepositCancellation { get; set; }

	public string CashHeldForOrders { get; set; }

	public string UnclearedDeposits { get; set; }

	public string Sma { get; set; }

	public string SmaHeldForOrders { get; set; }

	public string UnsettledFunds { get; set; }

	public string UnsettledDebit { get; set; }

	public string CryptoBuyingPower { get; set; }

	public string MaxAchEarlyAccessAmount { get; set; }

	public string CashBalances { get; set; }

	public MarginBalances MarginBalances { get; set; }

	public bool SweepEnabled { get; set; }

	public InstantEligibility InstantEligibility { get; set; }

	public string OptionLevel { get; set; }

	public bool IsPinnacleAccount { get; set; }

	public int RhsAccountNumber { get; set; }

	public string State { get; set; }

	public string ActiveSubscriptionId { get; set; }

	public bool Locked { get; set; }

	public bool PermanentlyDeactivated { get; set; }

	public bool ReceivedAchDebitLocked { get; set; }

	public bool DripEnabled { get; set; }

	public bool EligibleForFractionals { get; set; }

	public bool EligibleForDrip { get; set; }

	public bool EligibleForCashManagement { get; set; }

	public bool CashManagementEnabled { get; set; }

	public bool OptionTradingOnExpirationEnabled { get; set; }

	public string CashHeldForOptionsCollateral { get; set; }

	public bool FractionalPositionClosingOnly { get; set; }

	public string UserId { get; set; }

	public string RhsStockLoanConsentStatus { get; set; }

	public string EquityTradingLock { get; set; }

	public string OptionTradingLock { get; set; }
}
