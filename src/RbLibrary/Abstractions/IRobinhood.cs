using Rb.Integration.Api.Data.Base;
using System.Numerics;

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
	Task<Guid?> GetOptionQuoteAsync(string symbol, string strike, string expirationDate, OptionType optionType);

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
	Task<dynamic> GetOptionMarketDataAsync(Guid? optionId);

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
	/// Submits a market order to be executed immediately.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The number of stocks you want to buy.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderBuyMarketAsync(
		string symbol,
		int quantity,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);

	/// <summary>
	/// Submits a market order to be executed immediately for fractional shares by specifying the amount that you want to trade.
	/// Good for share fractions up to 6 decimal places.Robinhood does not currently support placing limit, stop, or stop loss orders
	/// for fractional trades.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The amount of the fractional shares you want to buy.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderBuyFractionalByQuantityAsync(
		string symbol,
		double quantity,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);

	/// <summary>
	/// Submits a market order to be executed immediately for fractional shares by specifying the amount in dollars that you want to trade.
	/// Good for share fractions up to 6 decimal places.Robinhood does not currently support placing limit, stop, or stop loss orders
	/// for fractional trades.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="amountInDollars">The amount in dollars of the fractional shares you want to buy.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <param name="marketHours">"regular_hours", "extended_hours" or "all_day_hours"</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderBuyFractionalByPriceAsync(
		string symbol,
		double amountInDollars,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false,
		string marketHours = "regular_hours");

	/// <summary>
	/// Submits a limit order to be executed once a certain price is reached.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The number of stocks to buy.</param>
	/// <param name="limitPrice">The price to trigger the buy order.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderBuyLimitAsync(
		string symbol,
		int quantity,
		double limitPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);

	/// <summary>
	/// Submits a stop order to be turned into a market order once a certain stop price is reached.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The amount of stocks to buy.</param>
	/// <param name="stopPrice">The price to trigger the market order.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderBuyStopLossAsync(
		string symbol,
		int quantity,
		double stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);

	/// <summary>
	/// Submits a stop order to be turned into a limit order once a certain stop price is reached.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The amount of stocks to buy.</param>
	/// <param name="limitPrice">The price to trigger the market order.</param>
	/// <param name="stopPrice">The price to trigger the limit order.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderBuyStopLimitAsync(
		string symbol,
		int quantity,
		double limitPrice,
		double stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);

	/// <summary>
	/// Submits a market order to be executed immediately.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The amount of stocks to sell.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderSellMarketAsync(
		string symbol,
		int quantity,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);

	/// <summary>
	/// Submits a market order to be executed immediately for fractional shares by specifying the amount that you want to trade.
	/// Good for share fractions up to 6 decimal places.Robinhood does not currently support placing limit, stop, or stop loss orders
	/// for fractional trades.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The amount of the fractional shares you want to sell.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <param name="marketHours">"regular_hours", "extended_hours" or "all_day_hours"</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderSellFractionalByQuantityAsync(
		string symbol,
		int quantity,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false,
		string marketHours = "regular_hours");

	/// <summary>
	/// Submits a market order to be executed immediately for fractional shares by specifying the amount in dollars that you want to trade.
	/// Good for share fractions up to 6 decimal places.Robinhood does not currently support placing limit, stop, or stop loss orders
	/// for fractional trades.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="amountInDollars">The amount in dollars of the fractional shares you want to buy.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderSellFractionalByPriceAsync(
		string symbol,
		double amountInDollars,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);

	/// <summary>
	/// Submits a limit order to be executed once a certain price is reached.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The amount of stocks to sell.</param>
	/// <param name="limitPrice">The price to trigger the sell order.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderSellLimitAsync(
		string symbol,
		int quantity,
		double limitPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);

	/// <summary>
	/// Submits a stop order to be turned into a market order once a certain stop price is reached.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The amount of stocks to sell.</param>
	/// <param name="stopPrice">The price to trigger the market order.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderSellStopLossAsync(
		string symbol,
		int quantity,
		double stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);

	/// <summary>
	/// Submits a stop order to be turned into a limit order once a certain stop price is reached.
	/// Reference: https://github.com/jmfernandes/robin_stocks/blob/master/robin_stocks/robinhood/orders.py
	/// </summary>
	/// <param name="symbol">The stock ticker of the stock.</param>
	/// <param name="quantity">The amount of stocks to sell.</param>
	/// <param name="limitPrice">The price to trigger the market order.</param>
	/// <param name="stopPrice">The price to trigger the limit order.</param>
	/// <param name="accountNumber">The Robinhood account number.</param>
	/// <param name="timeInForce">Changes how long the order will be in effect for. 'gtc' = good until cancelled. 'gfd' = good for the day.</param>
	/// <param name="extendedHours">Premium users only. Allows trading during extended hours. Should be true or false.</param>
	/// <returns>The placed order.</returns>
	Task<Order> PlaceOrderSellStopLimitAsync(
		string symbol,
		int quantity,
		double limitPrice,
		double stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false);
}
