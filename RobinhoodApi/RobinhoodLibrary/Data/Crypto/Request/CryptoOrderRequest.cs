using RobinhoodLibrary.Data.Base;
using System;

namespace RobinhoodLibrary.Data.Crypto.Request
{
    public class CryptoOrderRequest : BaseOrderRequest
    {
        public string AccountId { get; set; }

        public string CurrencyPairId { get; set; }

        public Guid RefId { get; set; } = Guid.NewGuid();
    }
}
