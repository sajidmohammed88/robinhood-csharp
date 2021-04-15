using RobinhoodLibrary.Data.Dividends;
using RobinhoodLibrary.Data.Fundamentals;
using RobinhoodLibrary.Data.Options;
using RobinhoodLibrary.Data.Orders;
using RobinhoodLibrary.Data.Portfolios;
using RobinhoodLibrary.Data.Positions;
using RobinhoodLibrary.Data.Quote;
using RobinhoodLibrary.Data.User;
using RobinhoodLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobinhoodLibrary.Abstractions
{
    /// <summary>
    /// The robinhood interface.
    /// </summary>
    public interface IRobinhood : IQuoteDataService, ISessionManager
    {
        /// <summary>
        /// Get the user.
        /// </summary>
        /// <returns>The user information.</returns>
        Task<User> GetUser();

        /// <summary>
        /// Get the investment profile.
        /// </summary>
        /// <returns>The investment profile.</returns>
        Task<InvestmentProfile> GetInvestmentProfile();

        /// <summary>
        /// Get the ask price for stock..
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns>The ask price.</returns>
        Task<string> AskPrice(string stock);

        /// <summary>
        /// Get bid price for stock.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns>The bid</returns>
        Task<string> BidPrice(string stock);

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <param name="expirationDates">The expiration dates.</param>
        /// <param name="optionType">Type of the option.</param>
        /// <returns>The options instrument chain.</returns>
        Task<IList<Option>> GetOptions(string stock, IList<string> expirationDates, OptionType optionType);

        /// <summary>
        /// Get the owned options.
        /// </summary>
        /// <returns>The owned options.</returns>
        Task<IList<Option>> GetOwnedOptions();

        /// <summary>
        /// Gets the option chain identifier by symbol.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns>The chain identifier.</returns>
        Task<Guid> GetOptionChainId(string symbol);

        /// <summary>
        /// Gets the option quote.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="strike">The strike.</param>
        /// <param name="expirationDate">The expiration date.</param>
        /// <param name="optionType">Type of the option.</param>
        /// <returns>Option quote</returns>
        Task<Guid> GetOptionQuote(string symbol, string strike, string expirationDate, OptionType optionType);

        /// <summary>
        /// Get the fundamentals.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns>Fundamentals.</returns>
        Task<Fundamental> GetFundamentals(string stock);

        /// <summary>
        /// Gets the portfolio.
        /// </summary>
        /// <returns>The portfolios</returns>
        Task<IList<Portfolio>> GetPortfolio();

        /// <summary>
        /// Gets the dividends.
        /// </summary>
        /// <returns>Dividends.</returns>
        Task<IList<Dividends>> GetDividends();

        /// <summary>
        /// Gets the positions.
        /// </summary>
        /// <returns>The positions data.</returns>
        Task<IList<Position>> GetPositions();

        /// <summary>
        /// Get list of securities symbols that the user has shares in.
        /// </summary>
        /// <returns>The owned securities.</returns>
        Task<IList<Position>> GetOwnedSecurities();

        /// <summary>
        /// Get a list of market data for a given option id.
        /// </summary>
        /// <param name="optionId">The option identifier.</param>
        /// <returns>The market data list.</returns>
        Task<dynamic> GetOptionMarketData(Guid optionId);

        /// <summary>
        /// Get the order history.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>The order history.</returns>
        Task<Order> GetOrderHistory(Guid orderId);

        /// <summary>
        /// Get the orders history.
        /// </summary>
        /// <returns>The orders history.</returns>
        Task<IList<Order>> GetOrdersHistory();

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
        Task<bool> CancelOrder(Guid orderId);

        /// <summary>
        /// Place the market buy order.
        /// </summary>
        /// <param name="instrumentUrl">The instrument URL.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="timeInForce">The time in force.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The quote data.</returns>
        Task<QuoteData> PlaceMarketBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity);

        /// <summary>
        /// Place the market sell order.
        /// </summary>
        /// <param name="instrumentUrl">The instrument URL.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="timeInForce">The time in force.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The quote data.</returns>
        Task<QuoteData> PlaceMarketSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity);

        /// <summary>
        /// Place the limit buy order.
        /// </summary>
        /// <param name="instrumentUrl">The instrument URL.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="timeInForce">The time in force.</param>
        /// <param name="price">The price.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The quote data.</returns>
        Task<QuoteData> PlaceLimitBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce, string price,
            int quantity);

        /// <summary>
        /// Place the limit sell order.
        /// </summary>
        /// <param name="instrumentUrl">The instrument URL.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="timeInForce">The time in force.</param>
        /// <param name="price">The price.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The quote data</returns>
        Task<QuoteData> PlaceLimitSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
            string price, int quantity);

        /// <summary>
        /// Place the stop loss buy order.
        /// </summary>
        /// <param name="instrumentUrl">The instrument URL.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="timeInForce">The time in force.</param>
        /// <param name="stopPrice">The stop price.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The quote data.</returns>
        Task<QuoteData> PlaceStopLossBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
            string stopPrice, int quantity);

        /// <summary>
        /// Place the stop loss sell order.
        /// </summary>
        /// <param name="instrumentUrl">The instrument URL.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="timeInForce">The time in force.</param>
        /// <param name="stopPrice">The stop price.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The quote data.</returns>
        Task<QuoteData> PlaceStopLossSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
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
        /// <returns>The quote data.</returns>
        Task<QuoteData> PlaceStopLimitBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
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
        /// <returns>The quote data.</returns>
        Task<QuoteData> PlaceStopLimitSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
            string price, string stopPrice, int quantity);

        /// <summary>
        /// Place the buy order.
        /// </summary>
        /// <param name="instrumentUrl">The instrument URL.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="price">The price.</param>
        /// <returns></returns>
        Task<dynamic> PlaceBuyOrder(string instrumentUrl, string symbol, string price);

        /// <summary>
        /// Place the sell order.
        /// </summary>
        /// <param name="instrumentUrl">The instrument URL.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="price">The price.</param>
        /// <returns></returns>
        Task<dynamic> PlaceSellOrder(string instrumentUrl, string symbol, string price);
    }
}
