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
	Task<IList<Order>> GetOrdersHistoryAsync();

	/// <summary>
	/// Gets the order history.
	/// </summary>
	/// <param name="orderId">The order identifier.</param>
	/// <returns>The order history.</returns>
	Task<Order> GetOrderHistoryAsync(Guid orderId);

	/// <summary>
	/// Submit the buy order.
	/// </summary>
	/// <param name="orderRequest">The order request.</param>
	/// <returns>The buy order.</returns>
	Task<Order> SubmitBuyOrderAsync(OrderRequest orderRequest);

	/// <summary>
	/// Submit the sell order.
	/// </summary>
	/// <param name="orderRequest">The order request.</param>
	/// <returns>The sell order.</returns>
	Task<Order> SubmitSellOrderAsync(OrderRequest orderRequest);

	/// <summary>
	/// Places the order.
	/// </summary>
	/// <param name="orderRequest">The order request.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderAsync(OrderRequest orderRequest);
}
