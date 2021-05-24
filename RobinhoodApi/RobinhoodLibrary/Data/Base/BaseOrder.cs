using RobinhoodLibrary.Data.Orders;
using RobinhoodLibrary.Enum;
using System;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Base
{
    public class BaseOrder
    {
        public string Detail { get; set; }

        public Guid Id { get; set; }

        public Guid? RefId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CumulativeQuantity { get; set; }

        public DateTime LastTransactionAt { get; set; }

        public string Price { get; set; }

        public string Quantity { get; set; }

        public Side Side { get; set; }

        public string State { get; set; }

        public TimeInForce TimeInForce { get; set; }

        public OrderType Type { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<Execution> Executions { get; set; }
    }
}
