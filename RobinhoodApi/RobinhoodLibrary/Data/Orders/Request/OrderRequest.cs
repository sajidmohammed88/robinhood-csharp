using RobinhoodLibrary.Enum;

namespace RobinhoodLibrary.Data.Orders.Request
{
    public class OrderRequest
    {
        public string InstrumentUrl { get; set; }

        public string Symbol { get; set; }

        public OrderType OrderType { get; set; }

        public TimeInForce TimeInForce { get; set; }

        public Trigger Trigger { get; set; }

        public string Price { get; set; }

        public string StopPrice { get; set; }

        public int Quantity { get; set; }

        public Side Side { get; set; }
    }
}
