using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.User
{
    public class Account
    {
        public string Url { get; set; }

        [JsonPropertyName("portfolio_cash")]
        public string PortfolioCash { get; set; }

        [JsonPropertyName("can_downgrade_to_cash")]
        public string CanDowngradeToCash { get; set; }

        public string User { get; set; }

        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }

        public string Type { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public bool Deactivated { get; set; }

        [JsonPropertyName("deposit_halted")]
        public bool DepositHalted { get; set; }

        [JsonPropertyName("withdrawal_halted")]
        public bool WithdrawalHalted { get; set; }

        [JsonPropertyName("only_position_closing_trades")]
        public bool OnlyPositionClosingTrades { get; set; }

        [JsonPropertyName("buying_power")]
        public string BuyingPower { get; set; }

        [JsonPropertyName("cash_available_for_withdrawal")]
        public string CashAvailableForWithdrawal { get; set; }

        public string Cash { get; set; }

        [JsonPropertyName("amount_eligible_for_deposit_cancellation")]
        public string AmountEligibleForDepositCancellation { get; set; }

        [JsonPropertyName("cash_held_for_orders")]
        public string CashHeldForOrders { get; set; }

        [JsonPropertyName("uncleared_deposits")]
        public string UnclearedDeposits { get; set; }

        public string Sma { get; set; }

        [JsonPropertyName("sma_held_for_orders")]
        public string SmaHeldForOrders { get; set; }

        [JsonPropertyName("unsettled_funds")]
        public string UnsettledFunds { get; set; }

        [JsonPropertyName("unsettled_debit")]
        public string UnsettledDebit { get; set; }

        [JsonPropertyName("crypto_buying_power")]
        public string CryptoBuyingPower { get; set; }

        [JsonPropertyName("max_ach_early_access_amount")]
        public string MaxAchEarlyAccessAmount { get; set; }

        [JsonPropertyName("cash_balances")]
        public string CashBalances { get; set; }

        [JsonPropertyName("margin_balances")]
        public MarginBalances MarginBalances { get; set; }

        [JsonPropertyName("sweep_enabled")]
        public bool SweepEnabled { get; set; }

        [JsonPropertyName("instant_eligibility")]
        public InstantEligibility InstantEligibility { get; set; }

        [JsonPropertyName("option_level")]
        public string OptionLevel { get; set; }

        [JsonPropertyName("is_pinnacle_account")]
        public bool IsPinnacleAccount { get; set; }

        [JsonPropertyName("rhs_account_number")]
        public int RhsAccountNumber { get; set; }

        public string State { get; set; }

        [JsonPropertyName("active_subscription_id")]
        public string ActiveSubscriptionId { get; set; }

        public bool Locked { get; set; }

        [JsonPropertyName("permanently_deactivated")]
        public bool PermanentlyDeactivated { get; set; }

        [JsonPropertyName("received_ach_debit_locked")]
        public bool ReceivedAchDebitLocked { get; set; }

        [JsonPropertyName("drip_enabled")]
        public bool DripEnabled { get; set; }

        [JsonPropertyName("eligible_for_fractionals")]
        public bool EligibleForFractionals { get; set; }

        [JsonPropertyName("eligible_for_drip")]
        public bool EligibleForDrip { get; set; }

        [JsonPropertyName("eligible_for_cash_management")]
        public bool EligibleForCashManagement { get; set; }

        [JsonPropertyName("cash_management_enabled")]
        public bool CashManagementEnabled { get; set; }

        [JsonPropertyName("option_trading_on_expiration_enabled")]
        public bool OptionTradingOnExpirationEnabled { get; set; }

        [JsonPropertyName("cash_held_for_options_collateral")]
        public string CashHeldForOptionsCollateral { get; set; }

        [JsonPropertyName("fractional_position_closing_only")]
        public bool FractionalPositionClosingOnly { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("rhs_stock_loan_consent_status")]
        public string RhsStockLoanConsentStatus { get; set; }

        [JsonPropertyName("equity_trading_lock")]
        public string EquityTradingLock { get; set; }

        [JsonPropertyName("option_trading_lock")]
        public string OptionTradingLock { get; set; }
    }
}
