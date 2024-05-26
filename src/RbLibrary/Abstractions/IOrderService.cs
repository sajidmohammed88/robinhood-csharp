using Rb.Integration.Api.Data.Orders;
using Rb.Integration.Api.Data.Orders.Request;

namespace Rb.Integration.Api.Abstractions;

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
	/// <returns>The buy order.</returns>
	Task<Order> SubmitBuyOrder(OrderRequest orderRequest);

	/// <summary>
	/// Submit the sell order.
	/// </summary>
	/// <param name="orderRequest">The order request.</param>
	/// <returns>The sell order.</returns>
	Task<Order> SubmitSellOrder(OrderRequest orderRequest);

	/// <summary>
	/// Places the order.
	/// </summary>
	/// <param name="orderRequest">The order request.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrder(OrderRequest orderRequest);
}
