using RobinhoodLibrary.Enum;
using System;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Orders
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid RefId { get; set; }

        public string Url { get; set; }

        public string Account { get; set; }

        public string Position { get; set; }

        public string Cancel { get; set; }

        public string Instrument { get; set; }

        public string CumulativeQuantity { get; set; }

        public string AveragePrice { get; set; }

        public string Fees { get; set; }

        public string State { get; set; }

        public OrderType Type { get; set; }

        public Side Side { get; set; }

        public TimeInForce TimeInForce { get; set; }

        public Trigger Trigger { get; set; }

        public string Price { get; set; }

        public string StopPrice { get; set; }

        public string Quantity { get; set; }

        public string RejectReason { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime LastTransactionAt { get; set; }

        public List<Execution> Executions { get; set; }

        public bool ExtendedHours { get; set; }

        public bool OverrideDtbpChecks { get; set; }

        public bool OverrideDayTradeChecks { get; set; }

        public string ResponseCategory { get; set; }

        public string StopTriggeredAt { get; set; }

        public string LastTrailPrice { get; set; }

        public DateTime? LastTrailPriceUpdatedAt { get; set; }

        public TotalNotional DollarBasedAmount { get; set; }

        public TotalNotional TotalNotional { get; set; }

        public TotalNotional ExecutedNotional { get; set; }

        public string InvestmentScheduleId { get; set; }

        public bool IsIpoAccessOrder { get; set; }

        public string IpoAccessCancellationReason { get; set; }

        public string IpoAccessLowerCollaredPrice { get; set; }

        public string IpoAccessUpperCollaredPrice { get; set; }

        public string IpoAccessUpperPrice { get; set; }

        public string IpoAccessLowerPrice { get; set; }

        public bool IsIpoAccessPriceFinalized { get; set; }
    }
}
