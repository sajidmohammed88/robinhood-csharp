using Rb.Integration.Api.Data.Dividends;
using Rb.Integration.Api.Data.Fundamentals;
using Rb.Integration.Api.Data.Options;
using Rb.Integration.Api.Data.Orders;
using Rb.Integration.Api.Data.Portfolios;
using Rb.Integration.Api.Data.Positions;
using Rb.Integration.Api.Data.User;
using Rb.Integration.Api.Enum;

namespace Rb.Integration.Api.Abstractions;

/// <summary>
/// The robinhood interface.
/// </summary>
public interface IRobinhood : IQuoteDataService, ISessionManager, ICryptoCurrencyService
{
	/// <summary>
	/// Get the user.
	/// </summary>
	/// <returns>The user information.</returns>
	Task<User> GetUserAsync();

	/// <summary>
	/// Get the investment profile.
	/// </summary>
	/// <returns>The investment profile.</returns>
	Task<InvestmentProfile> GetInvestmentProfileAsync();

	/// <summary>
	/// Get the ask price for stock..
	/// </summary>
	/// <param name="stock">The stock.</param>
	/// <returns>The ask price.</returns>
	Task<string> AskPriceAsync(string stock);

	/// <summary>
	/// Get bid price for stock.
	/// </summary>
	/// <param name="stock">The stock.</param>
	/// <returns>The bid</returns>
	Task<string> BidPriceAsync(string stock);

	/// <summary>
	/// Gets the options.
	/// </summary>
	/// <param name="stock">The stock.</param>
	/// <param name="expirationDates">The expiration dates.</param>
	/// <param name="optionType">Type of the option.</param>
	/// <returns>The options instrument chain.</returns>
	Task<IList<Option>> GetOptionsAsync(string stock, IList<string> expirationDates, OptionType optionType);

	/// <summary>
	/// Get the owned options.
	/// </summary>
	/// <returns>The owned options.</returns>
	Task<IList<Option>> GetOwnedOptionsAsync();

	/// <summary>
	/// Gets the option chain identifier by symbol.
	/// </summary>
	/// <param name="symbol">The symbol.</param>
	/// <returns>The chain identifier.</returns>
	Task<Guid> GetOptionChainIdAsync(string symbol);

	/// <summary>
	/// Gets the option quote.
	/// </summary>
	/// <param name="symbol">The symbol.</param>
	/// <param name="strike">The strike.</param>
	/// <param name="expirationDate">The expiration date.</param>
	/// <param name="optionType">Type of the option.</param>
	/// <returns>Option quote</returns>
	Task<Guid> GetOptionQuoteAsync(string symbol, string strike, string expirationDate, OptionType optionType);

	/// <summary>
	/// Get the fundamentals.
	/// </summary>
	/// <param name="stock">The stock.</param>
	/// <returns>Fundamentals.</returns>
	Task<Fundamental> GetFundamentalsAsync(string stock);

	/// <summary>
	/// Gets the portfolio.
	/// </summary>
	/// <returns>The portfolios</returns>
	Task<IList<Portfolio>> GetPortfolioAsync();

	/// <summary>
	/// Gets the dividends.
	/// </summary>
	/// <returns>Dividends.</returns>
	Task<IList<Dividends>> GetDividendsAsync();

	/// <summary>
	/// Gets the positions.
	/// </summary>
	/// <returns>The positions data.</returns>
	Task<IList<Position>> GetPositionsAsync();

	/// <summary>
	/// Get list of securities symbols that the user has shares in.
	/// </summary>
	/// <returns>The owned securities.</returns>
	Task<IList<Position>> GetOwnedSecuritiesAsync();

	/// <summary>
	/// Get a list of market data for a given option id.
	/// </summary>
	/// <param name="optionId">The option identifier.</param>
	/// <returns>The market data list.</returns>
	Task<dynamic> GetOptionMarketDataAsync(Guid optionId);

	/// <summary>
	/// Get the order history.
	/// </summary>
	/// <param name="orderId">The order identifier.</param>
	/// <returns>The order history.</returns>
	Task<Order> GetOrderHistoryAsync(Guid orderId);

	/// <summary>
	/// Get the orders history.
	/// </summary>
	/// <returns>The orders history.</returns>
	Task<IList<Order>> GetOrdersHistoryAsync();

	/// <summary>
	/// Gets the open orders.
	/// </summary>
	/// <returns>Open orders.</returns>
	Task<IList<Order>> GetOpenOrders();

	/// <summary>
	/// Cancel the order.
	/// </summary>
	/// <param name="orderId">The order identifier.</param>
	/// <returns>True if the order is canceled, otherwise false.</returns>
	Task<bool> CancelOrderAsync(Guid orderId);

	/// <summary>
	/// Place the market buy order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="timeInForce">The time in force.</param>
	/// <param name="quantity">The quantity.</param>
	/// <returns>The placed market buy order data.</returns>
	Task<Order> PlaceMarketBuyOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity);

	/// <summary>
	/// Place the market sell order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="timeInForce">The time in force.</param>
	/// <param name="quantity">The quantity.</param>
	/// <returns>The placed market sell order data.</returns>
	Task<Order> PlaceMarketSellOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity);

	/// <summary>
	/// Place the limit buy order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="timeInForce">The time in force.</param>
	/// <param name="price">The price.</param>
	/// <param name="quantity">The quantity.</param>
	/// <returns>The placed limit buy order data.</returns>
	Task<Order> PlaceLimitBuyOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce, string price,
		int quantity);

	/// <summary>
	/// Place the limit sell order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="timeInForce">The time in force.</param>
	/// <param name="price">The price.</param>
	/// <param name="quantity">The quantity.</param>
	/// <returns>The placed limit seller order data</returns>
	Task<Order> PlaceLimitSellOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, int quantity);

	/// <summary>
	/// Place the stop loss buy order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="timeInForce">The time in force.</param>
	/// <param name="stopPrice">The stop price.</param>
	/// <param name="quantity">The quantity.</param>
	/// <returns>The placed stop loss buy order data.</returns>
	Task<Order> PlaceStopLossBuyOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string stopPrice, int quantity);

	/// <summary>
	/// Place the stop loss sell order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="timeInForce">The time in force.</param>
	/// <param name="stopPrice">The stop price.</param>
	/// <param name="quantity">The quantity.</param>
	/// <returns>The placed stop loss seller order data.</returns>
	Task<Order> PlaceStopLossSellOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string stopPrice, int quantity);

	/// <summary>
	/// Place the stop limit buy order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="timeInForce">The time in force.</param>
	/// <param name="price">The price.</param>
	/// <param name="stopPrice">The stop price.</param>
	/// <param name="quantity">The quantity.</param>
	/// <returns>The placed stop limit buy order data.</returns>
	Task<Order> PlaceStopLimitBuyOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, string stopPrice, int quantity);

	/// <summary>
	/// Places the stop limit sell order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="timeInForce">The time in force.</param>
	/// <param name="price">The price.</param>
	/// <param name="stopPrice">The stop price.</param>
	/// <param name="quantity">The quantity.</param>
	/// <returns>The placed stop limit sell order data.</returns>
	Task<Order> PlaceStopLimitSellOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, string stopPrice, int quantity);

	/// <summary>
	/// Place the buy order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="price">The price.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceBuyOrderAsync(string instrumentUrl, string symbol, string price);

	/// <summary>
	/// Place the sell order.
	/// </summary>
	/// <param name="instrumentUrl">The instrument URL.</param>
	/// <param name="symbol">The symbol.</param>
	/// <param name="price">The price.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceSellOrderAsync(string instrumentUrl, string symbol, string price);
}
