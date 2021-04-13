using RobinhoodLibrary.Data.Orders;
using RobinhoodLibrary.Data.Orders.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobinhoodLibrary.Abstractions
{
    /// <summary>
    /// Order service that responsible on orders endpoints call.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Gets the orders history.
        /// </summary>
        /// <returns>The order history.</returns>
        Task<IList<Order>> GetOrdersHistory();

        /// <summary>
        /// Gets the order history.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>The order history.</returns>
        Task<Order> GetOrderHistory(Guid orderId);

        /// <summary>
        /// Submit the buy order.
        /// </summary>
        /// <param name="orderRequest">The order request.</param>
        /// <returns></returns>
        Task<dynamic> SubmitBuyOrder(OrderRequest orderRequest);

        /// <summary>
        /// Submit the sell order.
        /// </summary>
        /// <param name="orderRequest">The order request.</param>
        /// <returns></returns>
        Task<dynamic> SubmitSellOrder(OrderRequest orderRequest);

        /// <summary>
        /// Places the order.
        /// </summary>
        /// <param name="orderRequest">The order request.</param>
        /// <returns></returns>
        Task<dynamic> PlaceOrder(OrderRequest orderRequest);
    }
}
