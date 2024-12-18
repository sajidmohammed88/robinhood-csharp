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
	/// Places the order with params.
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The number of stocks.</param>
	/// <param name="side">Either Side.Buy or Side.Sell</param>
	/// <param name="limitPrice">The price to trigger the market order.</param>
	/// <param name="stopPrice">The price to trigger the sell.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <param name="marketHours">Choices are ['regular_hours', 'all_day_hours', 'extended_hours'].</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderAsync(
		string symbol,
		double quantity,
		Side side,
		double? limitPrice,
		double? stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false,
		string marketHours = "regular_hours");
}
