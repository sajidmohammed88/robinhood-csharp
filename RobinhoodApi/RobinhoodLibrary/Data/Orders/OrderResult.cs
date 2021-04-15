using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Orders
{
    public class OrderResult : BaseResult
    {
        public IList<Order> Results { get; set; }
    }
}
