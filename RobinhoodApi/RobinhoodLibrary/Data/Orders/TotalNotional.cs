using System;

namespace RobinhoodLibrary.Data.Orders
{
    public class TotalNotional
    {
        public string Amount { get; set; }

        public string CurrencyCode { get; set; }

        public Guid CurrencyId { get; set; }
    }
}
