using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.User
{
    public class MarginBalances
    {
        [JsonPropertyName("uncleared_deposits")]
        public string UnclearedDeposits { get; set; }

        public string Cash { get; set; }

        [JsonPropertyName("cash_held_for_dividends")]
        public string CashHeldForDividends { get; set; }

        [JsonPropertyName("cash_held_for_restrictions")]
        public string CashHeldForRestrictions { get; set; }

        [JsonPropertyName("cash_held_for_nummus_restrictions")]
        public string CashHeldForNummusRestrictions { get; set; }

        [JsonPropertyName("cash_held_for_orders")]
        public string CashHeldForOrders { get; set; }

        [JsonPropertyName("cash_available_for_withdrawal")]
        public string CashAvailableForWithdrawal { get; set; }

        [JsonPropertyName("unsettled_funds")]
        public string UnsettledFunds { get; set; }

        [JsonPropertyName("unsettled_debit")]
        public string UnsettledDebit { get; set; }

        [JsonPropertyName("outstanding_interest")]
        public string OutstandingInterest { get; set; }

        [JsonPropertyName("unallocated_margin_cash")]
        public string UnallocatedMarginCash { get; set; }

        [JsonPropertyName("margin_limit")]
        public string MarginLimit { get; set; }

        [JsonPropertyName("crypto_buying_power")]
        public string CryptoBuyingPower { get; set; }

        [JsonPropertyName("day_trade_buying_power")]
        public string DayTradeBuyingPower { get; set; }

        public string Sma { get; set; }

        [JsonPropertyName("day_trades_protection")]
        public bool DayTradesProtection { get; set; }

        [JsonPropertyName("start_of_day_overnight_buying_power")]
        public string StartOfDayOvernightBuyingPower { get; set; }

        [JsonPropertyName("overnight_buying_power")]
        public string OvernightBuyingPower { get; set; }

        [JsonPropertyName("overnight_buying_power_held_for_orders")]
        public string OvernightBuyingPowerHeldForOrders { get; set; }

        [JsonPropertyName("day_trade_buying_power_held_for_orders")]
        public string DayTradeBuyingPowerHeldForOrders { get; set; }

        [JsonPropertyName("overnight_ratio")]
        public string OvernightRatio { get; set; }

        [JsonPropertyName("day_trade_ratio")]
        public string DayTradeRatio { get; set; }

        [JsonPropertyName("marked_pattern_day_trader_date")]
        public string MarkedPatternDayTraderDate { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("start_of_day_dtbp")]
        public string StartOfDayDtbp { get; set; }

        [JsonPropertyName("portfolio_cash")]
        public string PortfolioCash { get; set; }

        [JsonPropertyName("cash_held_for_options_collateral")]
        public string CashHeldForOptionsCollateral { get; set; }

        [JsonPropertyName("gold_equity_requirement")]
        public string GoldEquityRequirement { get; set; }

        [JsonPropertyName("uncleared_nummus_deposits")]
        public string UnclearedNummusDeposits { get; set; }

        [JsonPropertyName("cash_pending_from_options_events")]
        public string CashPendingFromOptionsEvents { get; set; }

        [JsonPropertyName("settled_amount_borrowed")]
        public string SettledAmountBorrowed { get; set; }

        [JsonPropertyName("pending_deposit")]
        public string PendingDeposit { get; set; }

        [JsonPropertyName("funding_hold_balance")]
        public string FundingHoldBalance { get; set; }

        [JsonPropertyName("pending_debit_card_debits")]
        public string PendingDebitCardDebits { get; set; }

        [JsonPropertyName("net_moving_cash")]
        public string NetMovingCash { get; set; }

        [JsonPropertyName("margin_withdrawal_limit")]
        public string MarginWithdrawalLimit { get; set; }

        [JsonPropertyName("instant_used")]
        public string InstantUsed { get; set; }

        [JsonPropertyName("instant_allocated")]
        public string InstantAllocated { get; set; }

        [JsonPropertyName("eligible_deposit_as_instant")]
        public string EligibleDepositAsInstant { get; set; }

        [JsonPropertyName("leverage_enabled")]
        public bool LeverageEnabled { get; set; }
    }
}
