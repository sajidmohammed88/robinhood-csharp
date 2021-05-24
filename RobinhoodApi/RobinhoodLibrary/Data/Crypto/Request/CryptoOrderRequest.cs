using RobinhoodLibrary.Data.Base;

namespace RobinhoodLibrary.Data.Crypto.Request
{
    public class CryptoOrderRequest : BaseOrderRequest
    {
        public string Quantity { get; set; }
    }
}
