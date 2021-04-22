using RobinhoodLibrary.Data.Base;
using RobinhoodLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Orders
{
    public class Order : BaseDetail
    {
        public Guid Id { get; set; }

        [JsonPropertyName("ref_id")]
        public Guid? RefId { get; set; }

        public string Url { get; set; }

        [JsonPropertyName("account")]
        public string Account { get; set; }

        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("cancel")]
        public string Cancel { get; set; }

        [JsonPropertyName("instrument")]
        public string Instrument { get; set; }

        [JsonPropertyName("cumulative_quantity")]
        public string CumulativeQuantity { get; set; }

        [JsonPropertyName("average_price")]
        public string AveragePrice { get; set; }

        [JsonPropertyName("fees")]
        public string Fees { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("type")]
        public OrderType Type { get; set; }

        [JsonPropertyName("side")]
        public Side Side { get; set; }

        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }

        public Trigger Trigger { get; set; }

        public string Price { get; set; }

        [JsonPropertyName("stop_price")]
        public string StopPrice { get; set; }

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; }

        [JsonPropertyName("reject_reason")]
        public string RejectReason { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("last_transaction_at")]
        public DateTime LastTransactionAt { get; set; }

        public List<Execution> Executions { get; set; }

        [JsonPropertyName("extended_hours")]
        public bool ExtendedHours { get; set; }

        [JsonPropertyName("override_dtbp_checks")]
        public bool OverrideDtbpChecks { get; set; }

        [JsonPropertyName("override_day_trade_checks")]
        public bool OverrideDayTradeChecks { get; set; }

        [JsonPropertyName("response_category")]
        public string ResponseCategory { get; set; }

        [JsonPropertyName("stop_triggered_at")]
        public string StopTriggeredAt { get; set; }

        [JsonPropertyName("last_trail_price")]
        public string LastTrailPrice { get; set; }

        [JsonPropertyName("last_trail_price_updated_at")]
        public DateTime? LastTrailPriceUpdatedAt { get; set; }

        [JsonPropertyName("dollar_based_amount")]
        public TotalNotional DollarBasedAmount { get; set; }

        [JsonPropertyName("total_notional")]
        public TotalNotional TotalNotional { get; set; }

        [JsonPropertyName("executed_notional")]
        public TotalNotional ExecutedNotional { get; set; }

        [JsonPropertyName("investment_schedule_id")]
        public string InvestmentScheduleId { get; set; }

        [JsonPropertyName("is_ipo_access_order")]
        public bool IsIpoAccessOrder { get; set; }

        [JsonPropertyName("ipo_access_cancellation_reason")]
        public string IpoAccessCancellationReason { get; set; }

        [JsonPropertyName("ipo_access_lower_collared_price")]
        public string IpoAccessLowerCollaredPrice { get; set; }

        [JsonPropertyName("ipo_access_upper_collared_price")]
        public string IpoAccessUpperCollaredPrice { get; set; }

        [JsonPropertyName("ipo_access_upper_price")]
        public string IpoAccessUpperPrice { get; set; }

        [JsonPropertyName("ipo_access_lower_price")]
        public string IpoAccessLowerPrice { get; set; }

        [JsonPropertyName("is_ipo_access_price_finalized")]
        public bool IsIpoAccessPriceFinalized { get; set; }
    }
}
