using Rb.Integration.Api.Abstractions;
using Rb.Integration.Api.Data.Authentication;
using Rb.Integration.Api.Data.Base;
using Rb.Integration.Api.Data.Crypto;
using Rb.Integration.Api.Data.Crypto.Request;
using Rb.Integration.Api.Data.Dividends;
using Rb.Integration.Api.Data.Fundamentals;
using Rb.Integration.Api.Data.News;
using Rb.Integration.Api.Data.Options;
using Rb.Integration.Api.Data.Orders;
using Rb.Integration.Api.Data.Orders.Request;
using Rb.Integration.Api.Data.Portfolios;
using Rb.Integration.Api.Data.Positions;
using Rb.Integration.Api.Data.Quote;
using Rb.Integration.Api.Data.User;
using Rb.Integration.Api.Enum;
using Rb.Integration.Api.Exceptions;
using Rb.Integration.Api.Helpers;

using System.Net;

namespace Rb.Integration.Api;

public class Robinhood(ISessionManager sessionManager, IQuoteDataService quoteDataService,
	IOptionsInformationService optionsInformationService, IOrderService orderService, ICryptoCurrencyService cryptoCurrencyService,
	IHttpClientManager httpClientManager, IPaginator paginator) : IRobinhood
{
	#region USER

	/// <inheritdoc />
	public async Task<AuthenticationResponse> Login()
	{
		return await sessionManager.Login();
	}

	/// <inheritdoc />
	public async Task<AuthenticationResponse> ChallengeOauth2(Guid challengeId, string code)
	{
		return await sessionManager.ChallengeOauth2(challengeId, code);
	}

	/// <inheritdoc />
	public async Task<(HttpStatusCode, AuthenticationResponse)> MfaOath2(string code)
	{
		return await sessionManager.MfaOath2(code);
	}

	/// <inheritdoc />
	public void ConfigureManager(AuthenticationResponse response)
	{
		sessionManager.ConfigureManager(response);
	}

	/// <inheritdoc />
	public async Task Logout()
	{
		await sessionManager.Logout();
	}

	/// <inheritdoc />
	public async Task<User> GetUser()
	{
		return await httpClientManager.GetAsync<User>(Constants.Routes.User);
	}

	/// <inheritdoc />
	public async Task<InvestmentProfile> GetInvestmentProfile()
	{
		return await httpClientManager.GetAsync<InvestmentProfile>(Constants.Routes.InvestmentProfile);
	}
	#endregion
	#region QUOTEDATA

	/// <inheritdoc />
	public async Task<QuoteData> GetQuoteData(string stock)
	{
		return await quoteDataService.GetQuoteData(stock);
	}

	/// <inheritdoc />
	public async Task<IList<string>> GetQuoteWithSpecifiedKeys(string stock, string keys)
	{
		return await quoteDataService.GetQuoteWithSpecifiedKeys(stock, keys);
	}

	/// <inheritdoc />
	public async Task<string> AskPrice(string stock)
	{
		return (await quoteDataService.GetQuoteWithSpecifiedKeys(stock, "ask_price"))
			.FirstOrDefault();
	}

	/// <inheritdoc />
	public async Task<string> BidPrice(string stock)
	{
		return (await quoteDataService.GetQuoteWithSpecifiedKeys(stock, "bid_price"))
			.FirstOrDefault();
	}

	/// <inheritdoc />
	public async Task<IList<QuoteData>> GetQuotesData(IList<string> stocks)
	{
		return await quoteDataService.GetQuotesData(stocks);
	}

	/// <inheritdoc />
	public async Task<IDictionary<string, IList<string>>> GetQuotesWithSpecifiedKeys(IList<string> stocks, string keys)
	{
		return await quoteDataService.GetQuotesWithSpecifiedKeys(stocks, keys);
	}

	/// <inheritdoc />
	public async Task<IList<HistoricalsData>> GetHistoricalQuotes(IList<string> stocks, string interval, Span span, Bounds bounds = Bounds.Regular)
	{
		return await quoteDataService.GetHistoricalQuotes(stocks, interval, span, bounds);
	}

	/// <inheritdoc />
	public async Task<IList<string>> GetTickersByTag(string tag)
	{
		return await quoteDataService.GetTickersByTag(tag);
	}

	/// <inheritdoc />
	public async Task<Instrument> GetInstrument(string url)
	{
		return await quoteDataService.GetInstrument(url);
	}

	/// <inheritdoc />
	public async Task<IList<NewsData>> GetNews(string stock)
	{
		return await quoteDataService.GetNews(stock);
	}

	/// <inheritdoc />
	public async Task<Account> GetAccount()
	{
		return await quoteDataService.GetAccount();
	}

	/// <inheritdoc />
	public async Task<IList<Instrument>> GetWatchLists()
	{
		return await quoteDataService.GetWatchLists();
	}

	/// <inheritdoc />
	public async Task<dynamic> GetStockMarketData(IList<string> instruments)
	{
		return await quoteDataService.GetStockMarketData(instruments);
	}

	/// <inheritdoc />
	public async Task<dynamic> GetPopularity(string stock)
	{
		return await quoteDataService.GetPopularity(stock);
	}

	#endregion
	#region OPTIONS
	/// <inheritdoc />
	public async Task<IList<Option>> GetOptions(string stock, IList<string> expirationDates, OptionType optionType)
	{
		QuoteData quoteData = await quoteDataService.GetQuoteData(stock);

		if (string.IsNullOrEmpty(quoteData?.Instrument) || expirationDates == null || !expirationDates.Any())
		{
			throw new HttpResponseException("The instrument gotten by stock is null or empty");
		}

		string instrumentId = quoteData.Instrument.Split("/")[4];

		Chain chain = await optionsInformationService.GetChain(instrumentId);

		if (chain == null)
		{
			throw new HttpResponseException($"No chain result for the instrument : {instrumentId}");
		}

		return await optionsInformationService.GetOptionsByChainId(chain.Id, expirationDates, optionType);
	}

	/// <inheritdoc />
	public async Task<IList<Option>> GetOwnedOptions()
	{
		return await optionsInformationService.GetOwnedOptions();
	}

	/// <inheritdoc />
	public async Task<Guid> GetOptionChainId(string symbol)
	{
		return await optionsInformationService.GetOptionChainId(symbol);
	}

	/// <inheritdoc />
	public async Task<Guid> GetOptionQuote(string symbol, string strike, string expirationDate, OptionType optionType)
	{
		return await optionsInformationService.GetOptionQuote(symbol, strike, expirationDate, optionType);
	}

	/// <inheritdoc />
	public async Task<dynamic> GetOptionMarketData(Guid optionId)
	{
		return await optionsInformationService.GetOptionMarketData(optionId.ToString());
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
			return await httpClientManager.GetAsync<Fundamental>($"{Constants.Routes.FundamentalsBase}{stock.ToUpper()}/");
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
		PortfolioResult portfolioResult = await httpClientManager.GetAsync<PortfolioResult>(Constants.Routes.Portfolios);

		return portfolioResult.Results;
	}

	/// <inheritdoc />
	public async Task<IList<Dividends>> GetDividends()
	{
		BaseResult<Dividends> dividendsResult = await httpClientManager.GetAsync<BaseResult<Dividends>>(Constants.Routes.Dividends);
		return await paginator.PaginateResultAsync(dividendsResult);
	}
	#endregion
	#region POSITIONS

	/// <inheritdoc />
	public async Task<IList<Position>> GetPositions()
	{
		BaseResult<Position> positionResult = await httpClientManager
			.GetAsync<BaseResult<Position>>(Constants.Routes.Positions);

		return await paginator.PaginateResultAsync(positionResult);
	}

	/// <inheritdoc />
	public async Task<IList<Position>> GetOwnedSecurities()
	{
		BaseResult<Position> result = await httpClientManager
			.GetAsync<BaseResult<Position>>($"{Constants.Routes.Positions}?nonzero=true");

		return await paginator.PaginateResultAsync(result);
	}
	#endregion
	#region ORDER

	/// <inheritdoc />
	public async Task<Order> GetOrderHistory(Guid orderId)
	{
		return await orderService.GetOrderHistory(orderId);
	}

	/// <inheritdoc />
	public async Task<IList<Order>> GetOrdersHistory()
	{
		return await orderService.GetOrdersHistory();
	}

	/// <inheritdoc />
	public async Task<IList<Order>> GetOpenOrders()
	{
		IList<Order> orders = await orderService.GetOrdersHistory();
		return orders != null && orders.Any(o => o.Cancel != null)
			? orders.Where(o => o.Cancel != null).ToList()
			: new List<Order>();
	}

	/// <inheritdoc />
	public async Task<bool> CancelOrder(Guid orderId)
	{
		Order order = await orderService.GetOrderHistory(orderId);
		if (order == null)
		{
			throw new HttpResponseException($"The order {orderId} not exist.");
		}

		if (order.Cancel == null)
		{
			throw new HttpResponseException($"The order {orderId} can't be canceled.");
		}

		var (statusCode, _) = await httpClientManager.PostAsync(order.Cancel, null, (null, null));

		return statusCode == HttpStatusCode.OK;
	}

	/// <inheritdoc />
	public async Task<Order> PlaceMarketBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Market, Trigger.Immediate, Side.Buy);

		return await orderService.SubmitBuyOrder(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceMarketSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Market, Trigger.Immediate, Side.Sell);

		return await orderService.SubmitSellOrder(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceLimitBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Limit, Trigger.Immediate, Side.Buy, price);

		return await orderService.SubmitBuyOrder(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceLimitSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Limit, Trigger.Immediate, Side.Sell, price);

		return await orderService.SubmitSellOrder(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceStopLossBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string stopPrice, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Market, Trigger.Stop, Side.Buy, stopPrice: stopPrice);

		return await orderService.SubmitBuyOrder(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceStopLossSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string stopPrice, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Market, Trigger.Stop, Side.Sell, stopPrice: stopPrice);

		return await orderService.SubmitSellOrder(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceStopLimitBuyOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, string stopPrice, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Limit, Trigger.Stop, Side.Buy, price, stopPrice);

		return await orderService.SubmitBuyOrder(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceStopLimitSellOrder(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, string stopPrice, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Limit, Trigger.Stop, Side.Sell, price, stopPrice);

		return await orderService.SubmitBuyOrder(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceBuyOrder(string instrumentUrl, string symbol, string price)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, TimeInForce.Gfd, 1,
			OrderType.Market, Trigger.Immediate, Side.Buy, price ?? "0.0");

		return await orderService.PlaceOrder(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceSellOrder(string instrumentUrl, string symbol, string price)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, TimeInForce.Gfd, 1,
			OrderType.Market, Trigger.Immediate, Side.Sell, price ?? "0.0");

		return await orderService.PlaceOrder(orderRequest);
	}
	#endregion

	#region CRYPTO

	/// <inheritdoc />
	public async Task<IList<CurrencyPair>> GetCurrencyPairs()
	{
		return await cryptoCurrencyService.GetCurrencyPairs();
	}

	/// <inheritdoc />
	public async Task<Quotes> GetQuotes(string pair)
	{
		return await cryptoCurrencyService.GetQuotes(pair);
	}

	/// <inheritdoc />
	public async Task<IList<CryptoAccount>> GetAccounts()
	{
		return await cryptoCurrencyService.GetAccounts();
	}

	/// <inheritdoc />
	public async Task<CryptoOrder> Trade(string pair, CryptoOrderRequest orderRequest)
	{
		return await cryptoCurrencyService.Trade(pair, orderRequest);
	}

	/// <inheritdoc />
	public async Task<IList<CryptoOrder>> GetTradeHistory()
	{
		return await cryptoCurrencyService.GetTradeHistory();
	}

	/// <inheritdoc />
	public async Task<CryptoOrder> GetOrderStatus(string orderId)
	{
		return await cryptoCurrencyService.GetOrderStatus(orderId);
	}

	/// <inheritdoc />
	public async Task<bool> CancelCryptoOrder(string orderId)
	{
		return await cryptoCurrencyService.CancelCryptoOrder(orderId);
	}

	/// <inheritdoc />
	///:param pair: BTCUSD,ETHUSD
	///:param interval: optional 15second,5minute,10minute,hour,day,week
	///:param span: optional hour, day, year,5year,all
	///:param bounds: 24_7, regular, extended, trading
	public async Task<CryptoHistoricalData> Historicals(string pair, string interval, string span, string bounds)
	{
		return await cryptoCurrencyService.Historicals(pair, interval, span, bounds);
	}

	/// <inheritdoc />
	public async Task<IList<Holding>> Holdings()
	{
		return await cryptoCurrencyService.Holdings();
	}

	#endregion
}
