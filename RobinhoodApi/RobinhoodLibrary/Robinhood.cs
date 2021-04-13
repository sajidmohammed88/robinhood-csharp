using RobinhoodLibrary.Abstractions;
using RobinhoodLibrary.Data.Authentication;
using RobinhoodLibrary.Data.Fundamentals;
using RobinhoodLibrary.Data.News;
using RobinhoodLibrary.Data.Options;
using RobinhoodLibrary.Data.Orders;
using RobinhoodLibrary.Data.Orders.Request;
using RobinhoodLibrary.Data.Portfolios;
using RobinhoodLibrary.Data.Positions;
using RobinhoodLibrary.Data.Quote;
using RobinhoodLibrary.Data.User;
using RobinhoodLibrary.Enum;
using RobinhoodLibrary.Exceptions;
using RobinhoodLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RobinhoodLibrary
{
    public class Robinhood : IRobinhood
    {
        private readonly IHttpClientManager _httpClientManager;
        private readonly ISessionManager _sessionManager;
        private readonly IQuoteDataService _quoteDataService;
        private readonly IOptionsInformationService _optionsInformationService;
        private readonly IOrderService _orderService;

        public Robinhood(ISessionManager sessionManager, IQuoteDataService quoteDataService,
            IOptionsInformationService optionsInformationService, IOrderService orderService, IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager ?? throw new ArgumentNullException(nameof(httpClientManager));
            _sessionManager = sessionManager ?? throw new ArgumentNullException(nameof(sessionManager));
            _quoteDataService = quoteDataService ?? throw new ArgumentNullException(nameof(sessionManager));
            _optionsInformationService = optionsInformationService ?? throw new ArgumentNullException(nameof(optionsInformationService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        #region USER

        /// <inheritdoc />
        public async Task<AuthenticationResponse> Login()
        {
            return await _sessionManager.Login();
        }

        /// <inheritdoc />
        public async Task<AuthenticationResponse> ChallengeOauth2(Guid challengeId, string code)
        {
            return await _sessionManager.ChallengeOauth2(challengeId, code);
        }

        /// <inheritdoc />
        public async Task<(HttpStatusCode, AuthenticationResponse)> MfaOath2(string code)
        {
            return await _sessionManager.MfaOath2(code);
        }

        /// <inheritdoc />
        public void ConfigureManager(AuthenticationResponse response)
        {
            _sessionManager.ConfigureManager(response);
        }

        /// <inheritdoc />
        public async Task Logout()
        {
            await _sessionManager.Logout();
        }

        /// <inheritdoc />
        public async Task<User> GetUser()
        {
            return await _httpClientManager.GetAsync<User>(Constants.Routes.User);
        }

        /// <inheritdoc />
        public async Task<InvestmentProfile> GetInvestmentProfile()
        {
            return await _httpClientManager.GetAsync<InvestmentProfile>(Constants.Routes.InvestmentProfile);
        }
        #endregion
        #region QUOTEDATA

        /// <inheritdoc />
        public async Task<QuoteData> GetQuoteData(string stock)
        {
            return await _quoteDataService.GetQuoteData(stock);
        }

        /// <inheritdoc />
        public async Task<IList<string>> GetQuoteWithSpecifiedKeys(string stock, string keys)
        {
            return await _quoteDataService.GetQuoteWithSpecifiedKeys(stock, keys);
        }

        /// <inheritdoc />
        public async Task<string> AskPrice(string stock)
        {
            return (await _quoteDataService.GetQuoteWithSpecifiedKeys(stock, "ask_price"))
                .FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<string> BidPrice(string stock)
        {
            return (await _quoteDataService.GetQuoteWithSpecifiedKeys(stock, "bid_price"))
                .FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<IList<QuoteData>> GetQuotesData(IList<string> stocks)
        {
            return await _quoteDataService.GetQuotesData(stocks);
        }

        /// <inheritdoc />
        public async Task<IDictionary<string, IList<string>>> GetQuotesWithSpecifiedKeys(IList<string> stocks, string keys)
        {
            return await _quoteDataService.GetQuotesWithSpecifiedKeys(stocks, keys);
        }

        /// <inheritdoc />
        public async Task<IList<HistoricalsData>> GetHistoricalQuotes(IList<string> stocks, string interval, Span span, Bounds bounds = Bounds.Regular)
        {
            return await _quoteDataService.GetHistoricalQuotes(stocks, interval, span, bounds);
        }

        /// <inheritdoc />
        public async Task<IList<string>> GetTickersByTag(string tag)
        {
            return await _quoteDataService.GetTickersByTag(tag);
        }

        /// <inheritdoc />
        public async Task<Instrument> GetInstrument(string url)
        {
            return await _quoteDataService.GetInstrument(url);
        }

        /// <inheritdoc />
        public async Task<IList<NewsData>> GetNews(string stock)
        {
            return await _quoteDataService.GetNews(stock);
        }

        /// <inheritdoc />
        public async Task<Account> GetAccount()
        {
            return await _quoteDataService.GetAccount();
        }

        /// <inheritdoc />
        public async Task<IList<Instrument>> GetWatchLists()
        {
            return await _quoteDataService.GetWatchLists();
        }

        /// <inheritdoc />
        public async Task<dynamic> GetStockMarketData(IList<string> instruments)
        {
            return await _quoteDataService.GetStockMarketData(instruments);
        }

        /// <inheritdoc />
        public async Task<dynamic> GetPopularity(string stock)
        {
            return await _quoteDataService.GetPopularity(stock);
        }

        #endregion
        #region OPTIONS
        /// <inheritdoc />
        public async Task<IList<Option>> GetOptions(string stock, IList<string> expirationDates, OptionType optionType)
        {
            QuoteData quoteData = await _quoteDataService.GetQuoteData(stock);

            if (string.IsNullOrEmpty(quoteData?.Instrument) || expirationDates == null || !expirationDates.Any())
            {
                throw new HttpResponseException("The instrument gotten by stock is null or empty");
            }

            string instrumentId = quoteData.Instrument.Split("/")[4];

            Chain chain = await _optionsInformationService.GetChain(instrumentId);

            if (chain == null)
            {
                throw new HttpResponseException($"No chain result for the instrument : {instrumentId}");
            }

            return await _optionsInformationService.GetOptionsByChainId(chain.Id, expirationDates, optionType);
        }

        /// <inheritdoc />
        public async Task<IList<Option>> GetOwnedOptions()
        {
            return await _optionsInformationService.GetOwnedOptions();
        }

        /// <inheritdoc />
        public async Task<Guid> GetOptionChainId(string symbol)
        {
            return await _optionsInformationService.GetOptionChainId(symbol);
        }

        /// <inheritdoc />
        public async Task<Guid> GetOptionQuote(string symbol, string strike, string expirationDate, OptionType optionType)
        {
            return await _optionsInformationService.GetOptionQuote(symbol, strike, expirationDate, optionType);
        }

        /// <inheritdoc />
        public async Task<dynamic> GetOptionMarketData(Guid optionId)
        {
            return await _optionsInformationService.GetOptionMarketData(optionId.ToString());
        }
        #endregion
        #region FUNDAMENTALS

        /// <inheritdoc />
        public async Task<Fundamental> GetFundamentals(string stock)
        {
            if (string.IsNullOrEmpty(stock))
            {
                throw new HttpResponseException("The fundamentals stock request is null or empty");
            }

            try
            {
                return await _httpClientManager.GetAsync<Fundamental>($"{Constants.Routes.FundamentalsBase}{stock.ToUpper()}/");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Invalid tickers symbol, reason : {ex.Message}");
            }
        }
        #endregion
        #region PORTFOLIO

        /// <inheritdoc />
        public async Task<IList<Portfolio>> GetPortfolio()
        {
            PortfolioResult portfolioResult = await _httpClientManager.GetAsync<PortfolioResult>(Constants.Routes.Portfolios);

            return portfolioResult.Results;
        }

        /// <inheritdoc />
        public async Task<dynamic> GetDividends()
        {
            return await _httpClientManager.GetAsync<dynamic>(Constants.Routes.Dividends);
        }
        #endregion
        #region POSITIONS

        /// <inheritdoc />
        public async Task<IList<Position>> GetPositions()
        {
            PositionResult positionResult = await _httpClientManager
                .GetAsync<PositionResult>(Constants.Routes.Positions);

            return await FillPaginatedPotions(positionResult);
        }

        /// <inheritdoc />
        public async Task<IList<Position>> GetOwnedSecurities()
        {
            PositionResult result = await _httpClientManager
                .GetAsync<PositionResult>($"{Constants.Routes.Positions}?nonzero=true");

            return await FillPaginatedPotions(result);
        }

        private async Task<IList<Position>> FillPaginatedPotions(PositionResult positionResult)
        {
            List<Position> positions = new List<Position>();
            if (positionResult?.Results == null || !positionResult.Results.Any())
            {
                return positions;
            }
            positions.AddRange(positionResult.Results);

            while (positionResult.Next != null)
            {
                positionResult = await _httpClientManager.GetAsync<PositionResult>(positionResult.Next);
                positions.AddRange(positionResult.Results);
            }

            return positions;
        }
        #endregion
        #region ORDER

        /// <inheritdoc />
        public async Task<Order> GetOrderHistory(Guid orderId)
        {
            return await _orderService.GetOrderHistory(orderId);
        }

        /// <inheritdoc />
        public async Task<IList<Order>> GetOrdersHistory()
        {
            return await _orderService.GetOrdersHistory();
        }

        /// <inheritdoc />
        public async Task<IList<Order>> GetOpenOrders()
        {
            IList<Order> orders = await _orderService.GetOrdersHistory();
            return orders != null && orders.Any(o => o.Cancel != null)
                ? orders.Where(o => o.Cancel != null).ToList()
                : new List<Order>();
        }

        /// <inheritdoc />
        public async Task CancelOrder(Guid orderId)
        {
            Order order = await _orderService.GetOrderHistory(orderId);
            if (order == null)
            {
                throw new HttpResponseException($"No order exist for the order id : {orderId}");
            }

            await _httpClientManager.PostAsync(order.Cancel, null, (null, null));
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceMarketBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity);
            orderRequest.OrderType = OrderType.Market;
            orderRequest.Trigger = Trigger.Immediate;
            orderRequest.Side = Side.Buy;

            return await _orderService.SubmitBuyOrder(orderRequest);
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceMarketSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity);
            orderRequest.OrderType = OrderType.Market;
            orderRequest.Trigger = Trigger.Immediate;
            orderRequest.Side = Side.Sell;

            return await _orderService.SubmitSellOrder(orderRequest);
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceLimitBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
            string price, int quantity)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity, price);
            orderRequest.OrderType = OrderType.Limit;
            orderRequest.Trigger = Trigger.Immediate;
            orderRequest.Side = Side.Buy;

            return await _orderService.SubmitBuyOrder(orderRequest);
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceLimitSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
            string price, int quantity)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity, price);
            orderRequest.OrderType = OrderType.Limit;
            orderRequest.Trigger = Trigger.Immediate;
            orderRequest.Side = Side.Sell;

            return await _orderService.SubmitBuyOrder(orderRequest);
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceStopLossBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
            string stopPrice, int quantity)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity, stopPrice: stopPrice);
            orderRequest.OrderType = OrderType.Market;
            orderRequest.Trigger = Trigger.Stop;
            orderRequest.Side = Side.Buy;

            return await _orderService.SubmitBuyOrder(orderRequest);
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceStopLossSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
            string stopPrice, int quantity)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity, stopPrice: stopPrice);
            orderRequest.OrderType = OrderType.Market;
            orderRequest.Trigger = Trigger.Stop;
            orderRequest.Side = Side.Sell;

            return await _orderService.SubmitBuyOrder(orderRequest);
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceStopLimitBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
            string price, string stopPrice, int quantity)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity, price, stopPrice);
            orderRequest.OrderType = OrderType.Limit;
            orderRequest.Trigger = Trigger.Stop;
            orderRequest.Side = Side.Buy;

            return await _orderService.SubmitBuyOrder(orderRequest);
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceStopLimitSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
            string price, string stopPrice, int quantity)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity, price, stopPrice);
            orderRequest.OrderType = OrderType.Limit;
            orderRequest.Trigger = Trigger.Stop;
            orderRequest.Side = Side.Sell;

            return await _orderService.SubmitBuyOrder(orderRequest);
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceBuyOrder(string instrumentUrl, string symbol, string price)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, TimeInForce.Gfd, 1, price ?? "0.0");
            orderRequest.OrderType = OrderType.Market;
            orderRequest.Trigger = Trigger.Immediate;
            orderRequest.Side = Side.Buy;

            return await _orderService.PlaceOrder(orderRequest);
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceSellOrder(string instrumentUrl, string symbol, string price)
        {
            OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, TimeInForce.Gfd, 1, price ?? "0.0");
            orderRequest.OrderType = OrderType.Market;
            orderRequest.Trigger = Trigger.Immediate;
            orderRequest.Side = Side.Sell;

            return await _orderService.PlaceOrder(orderRequest);
        }
        #endregion
    }
}
