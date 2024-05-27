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
	public async Task<AuthenticationResponse> LoginAsync()
	{
		return await sessionManager.LoginAsync();
	}

	/// <inheritdoc />
	public async Task<AuthenticationResponse> ChallengeOauth2Async(Guid challengeId, string code)
	{
		return await sessionManager.ChallengeOauth2Async(challengeId, code);
	}

	/// <inheritdoc />
	public async Task<(HttpStatusCode, AuthenticationResponse)> MfaOath2Async(string code)
	{
		return await sessionManager.MfaOath2Async(code);
	}

	/// <inheritdoc />
	public void ConfigureManager(AuthenticationResponse response)
	{
		sessionManager.ConfigureManager(response);
	}

	/// <inheritdoc />
	public async Task LogoutAsync()
	{
		await sessionManager.LogoutAsync();
	}

	/// <inheritdoc />
	public async Task<User> GetUserAsync()
	{
		return await httpClientManager.GetAsync<User>(Constants.Routes.User);
	}

	/// <inheritdoc />
	public async Task<InvestmentProfile> GetInvestmentProfileAsync()
	{
		return await httpClientManager.GetAsync<InvestmentProfile>(Constants.Routes.InvestmentProfile);
	}
	#endregion
	#region QUOTEDATA

	/// <inheritdoc />
	public async Task<QuoteData> GetQuoteDataAsync(string stock)
	{
		return await quoteDataService.GetQuoteDataAsync(stock);
	}

	/// <inheritdoc />
	public async Task<IList<string>> GetQuoteWithSpecifiedKeysAsync(string stock, string keys)
	{
		return await quoteDataService.GetQuoteWithSpecifiedKeysAsync(stock, keys);
	}

	/// <inheritdoc />
	public async Task<string> AskPriceAsync(string stock)
	{
		return (await quoteDataService.GetQuoteWithSpecifiedKeysAsync(stock, "ask_price"))
			.FirstOrDefault();
	}

	/// <inheritdoc />
	public async Task<string> BidPriceAsync(string stock)
	{
		return (await quoteDataService.GetQuoteWithSpecifiedKeysAsync(stock, "bid_price"))
			.FirstOrDefault();
	}

	/// <inheritdoc />
	public async Task<IList<QuoteData>> GetQuotesDataAsync(IList<string> stocks)
	{
		return await quoteDataService.GetQuotesDataAsync(stocks);
	}

	/// <inheritdoc />
	public async Task<IDictionary<string, IList<string>>> GetQuotesWithSpecifiedKeysAsync(IList<string> stocks, string keys)
	{
		return await quoteDataService.GetQuotesWithSpecifiedKeysAsync(stocks, keys);
	}

	/// <inheritdoc />
	public async Task<IList<HistoricalsData>> GetHistoricalQuotesAsync(IList<string> stocks, string interval, Span span, Bounds bounds = Bounds.Regular)
	{
		return await quoteDataService.GetHistoricalQuotesAsync(stocks, interval, span, bounds);
	}

	/// <inheritdoc />
	public async Task<IList<string>> GetTickersByTagAsync(string tag)
	{
		return await quoteDataService.GetTickersByTagAsync(tag);
	}

	/// <inheritdoc />
	public async Task<Instrument> GetInstrumentAsync(string url)
	{
		return await quoteDataService.GetInstrumentAsync(url);
	}

	/// <inheritdoc />
	public async Task<IList<NewsData>> GetNewsAsync(string stock)
	{
		return await quoteDataService.GetNewsAsync(stock);
	}

	/// <inheritdoc />
	public async Task<Account> GetAccountAsync()
	{
		return await quoteDataService.GetAccountAsync();
	}

	/// <inheritdoc />
	public async Task<IList<Instrument>> GetWatchListsAsync()
	{
		return await quoteDataService.GetWatchListsAsync();
	}

	/// <inheritdoc />
	public async Task<dynamic> GetStockMarketDataAsync(IList<string> instruments)
	{
		return await quoteDataService.GetStockMarketDataAsync(instruments);
	}

	/// <inheritdoc />
	public async Task<dynamic> GetPopularityAsync(string stock)
	{
		return await quoteDataService.GetPopularityAsync(stock);
	}

	#endregion
	#region OPTIONS
	/// <inheritdoc />
	public async Task<IList<Option>> GetOptionsAsync(string stock, IList<string> expirationDates, OptionType optionType)
	{
		QuoteData quoteData = await quoteDataService.GetQuoteDataAsync(stock);

		if (string.IsNullOrEmpty(quoteData?.Instrument) || expirationDates == null || !expirationDates.Any())
		{
			throw new HttpResponseException("The instrument gotten by stock is null or empty");
		}

		string instrumentId = quoteData.Instrument.Split("/")[4];

		Chain chain = await optionsInformationService.GetChainAsync(instrumentId);

		if (chain == null)
		{
			throw new HttpResponseException($"No chain result for the instrument : {instrumentId}");
		}

		return await optionsInformationService.GetOptionsByChainIdAsync(chain.Id, expirationDates, optionType);
	}

	/// <inheritdoc />
	public async Task<IList<Option>> GetOwnedOptionsAsync()
	{
		return await optionsInformationService.GetOwnedOptionsAsync();
	}

	/// <inheritdoc />
	public async Task<Guid> GetOptionChainIdAsync(string symbol)
	{
		return await optionsInformationService.GetOptionChainIdAsync(symbol);
	}

	/// <inheritdoc />
	public async Task<Guid> GetOptionQuoteAsync(string symbol, string strike, string expirationDate, OptionType optionType)
	{
		return await optionsInformationService.GetOptionQuoteAsync(symbol, strike, expirationDate, optionType);
	}

	/// <inheritdoc />
	public async Task<dynamic> GetOptionMarketDataAsync(Guid optionId)
	{
		return await optionsInformationService.GetOptionMarketDataAsync(optionId.ToString());
	}
	#endregion

	#region FUNDAMENTALS

	/// <inheritdoc />
	public async Task<Fundamental> GetFundamentalsAsync(string stock)
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
	public async Task<IList<Portfolio>> GetPortfolioAsync()
	{
		PortfolioResult portfolioResult = await httpClientManager.GetAsync<PortfolioResult>(Constants.Routes.Portfolios);

		return portfolioResult.Results;
	}

	/// <inheritdoc />
	public async Task<IList<Dividends>> GetDividendsAsync()
	{
		BaseResult<Dividends> dividendsResult = await httpClientManager.GetAsync<BaseResult<Dividends>>(Constants.Routes.Dividends);
		return await paginator.PaginateResultAsync(dividendsResult);
	}
	#endregion
	#region POSITIONS

	/// <inheritdoc />
	public async Task<IList<Position>> GetPositionsAsync()
	{
		BaseResult<Position> positionResult = await httpClientManager
			.GetAsync<BaseResult<Position>>(Constants.Routes.Positions);

		return await paginator.PaginateResultAsync(positionResult);
	}

	/// <inheritdoc />
	public async Task<IList<Position>> GetOwnedSecuritiesAsync()
	{
		BaseResult<Position> result = await httpClientManager
			.GetAsync<BaseResult<Position>>($"{Constants.Routes.Positions}?nonzero=true");

		return await paginator.PaginateResultAsync(result);
	}
	#endregion
	#region ORDER

	/// <inheritdoc />
	public async Task<Order> GetOrderHistoryAsync(Guid orderId)
	{
		return await orderService.GetOrderHistoryAsync(orderId);
	}

	/// <inheritdoc />
	public async Task<IList<Order>> GetOrdersHistoryAsync()
	{
		return await orderService.GetOrdersHistoryAsync();
	}

	/// <inheritdoc />
	public async Task<IList<Order>> GetOpenOrders()
	{
		IList<Order> orders = await orderService.GetOrdersHistoryAsync();
		return orders != null && orders.Any(o => o.Cancel != null)
			? orders.Where(o => o.Cancel != null).ToList()
			: [];
	}

	/// <inheritdoc />
	public async Task<bool> CancelOrderAsync(Guid orderId)
	{
		Order order = await orderService.GetOrderHistoryAsync(orderId);
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
	public async Task<Order> PlaceMarketBuyOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Market, Trigger.Immediate, Side.Buy);

		return await orderService.SubmitBuyOrderAsync(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceMarketSellOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Market, Trigger.Immediate, Side.Sell);

		return await orderService.SubmitSellOrderAsync(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceLimitBuyOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Limit, Trigger.Immediate, Side.Buy, price);

		return await orderService.SubmitBuyOrderAsync(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceLimitSellOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Limit, Trigger.Immediate, Side.Sell, price);

		return await orderService.SubmitSellOrderAsync(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceStopLossBuyOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string stopPrice, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Market, Trigger.Stop, Side.Buy, stopPrice: stopPrice);

		return await orderService.SubmitBuyOrderAsync(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceStopLossSellOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string stopPrice, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Market, Trigger.Stop, Side.Sell, stopPrice: stopPrice);

		return await orderService.SubmitSellOrderAsync(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceStopLimitBuyOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, string stopPrice, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Limit, Trigger.Stop, Side.Buy, price, stopPrice);

		return await orderService.SubmitBuyOrderAsync(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceStopLimitSellOrderAsync(string instrumentUrl, string symbol, TimeInForce timeInForce,
		string price, string stopPrice, int quantity)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, timeInForce, quantity,
			OrderType.Limit, Trigger.Stop, Side.Sell, price, stopPrice);

		return await orderService.SubmitBuyOrderAsync(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceBuyOrderAsync(string instrumentUrl, string symbol, string price)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, TimeInForce.Gfd, 1,
			OrderType.Market, Trigger.Immediate, Side.Buy, price ?? "0.0");

		return await orderService.PlaceOrderAsync(orderRequest);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceSellOrderAsync(string instrumentUrl, string symbol, string price)
	{
		OrderRequest orderRequest = RbHelper.BuildOrderRequestForMarket(instrumentUrl, symbol, TimeInForce.Gfd, 1,
			OrderType.Market, Trigger.Immediate, Side.Sell, price ?? "0.0");

		return await orderService.PlaceOrderAsync(orderRequest);
	}
	#endregion

	#region CRYPTO

	/// <inheritdoc />
	public async Task<IList<CurrencyPair>> GetCurrencyPairsAsync()
	{
		return await cryptoCurrencyService.GetCurrencyPairsAsync();
	}

	/// <inheritdoc />
	public async Task<Quotes> GetQuotesAsync(string pair)
	{
		return await cryptoCurrencyService.GetQuotesAsync(pair);
	}

	/// <inheritdoc />
	public async Task<IList<CryptoAccount>> GetAccountsAsync()
	{
		return await cryptoCurrencyService.GetAccountsAsync();
	}

	/// <inheritdoc />
	public async Task<CryptoOrder> TradeAsync(string pair, CryptoOrderRequest orderRequest)
	{
		return await cryptoCurrencyService.TradeAsync(pair, orderRequest);
	}

	/// <inheritdoc />
	public async Task<IList<CryptoOrder>> GetTradeHistoryAsync()
	{
		return await cryptoCurrencyService.GetTradeHistoryAsync();
	}

	/// <inheritdoc />
	public async Task<CryptoOrder> GetOrderStatusAsync(string orderId)
	{
		return await cryptoCurrencyService.GetOrderStatusAsync(orderId);
	}

	/// <inheritdoc />
	public async Task<bool> CancelCryptoOrderAsync(string orderId)
	{
		return await cryptoCurrencyService.CancelCryptoOrderAsync(orderId);
	}

	/// <inheritdoc />
	///:param pair: BTCUSD,ETHUSD
	///:param interval: optional 15second,5minute,10minute,hour,day,week
	///:param span: optional hour, day, year,5year,all
	///:param bounds: 24_7, regular, extended, trading
	public async Task<CryptoHistoricalData> HistoricalsAsync(string pair, string interval, string span, string bounds)
	{
		return await cryptoCurrencyService.HistoricalsAsync(pair, interval, span, bounds);
	}

	/// <inheritdoc />
	public async Task<IList<Holding>> HoldingsAsync()
	{
		return await cryptoCurrencyService.HoldingsAsync();
	}

	#endregion
}
