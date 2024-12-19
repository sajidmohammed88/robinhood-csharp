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
	public async Task<AuthenticationResponse> ChallengeOauth2Async(Guid? challengeId, string code)
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
		return (await quoteDataService.GetQuoteWithSpecifiedKeysAsync(stock, "AskPrice"))
			.FirstOrDefault();
	}

	/// <inheritdoc />
	public async Task<string> BidPriceAsync(string stock)
	{
		return (await quoteDataService.GetQuoteWithSpecifiedKeysAsync(stock, "BidPrice"))
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
	public async Task<Guid?> GetOptionQuoteAsync(string symbol, string strike, string expirationDate, OptionType optionType)
	{
		return await optionsInformationService.GetOptionQuoteAsync(symbol, strike, expirationDate, optionType);
	}

	/// <inheritdoc />
	public async Task<dynamic> GetOptionMarketDataAsync(Guid? optionId)
	{
		return await optionsInformationService.GetOptionMarketDataAsync(optionId?.ToString());
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
		Order order = await orderService.GetOrderHistoryAsync(orderId) ?? throw new HttpResponseException($"The order {orderId} not exist.");
		if (order.Cancel == null)
		{
			throw new HttpResponseException($"The order {orderId} can't be canceled.");
		}

		(HttpStatusCode statusCode, string _) = await httpClientManager.PostAsync(order.Cancel, null, (null, null));

		return statusCode == HttpStatusCode.OK;
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderBuyMarketAsync(
		string symbol,
		int quantity,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Buy,
			null,
			null,
			accountNumber,
			timeInForce,
			extendedHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderBuyFractionalByQuantityAsync(
		string symbol,
		double quantity,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Buy,
			null,
			null,
			accountNumber,
			timeInForce,
			extendedHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderBuyFractionalByPriceAsync(
		string symbol,
		double amountInDollars,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false,
		string marketHours = "regular_hours")
	{
		if (amountInDollars < 1)
		{
			throw new HttpResponseException("Fractional share price should meet minimum 1.00");
		}

		_ = double.TryParse(await AskPriceAsync(symbol), out double priceValue);
		double fractionalShares = priceValue == 0 ? 0.0 : MathHelper.RoundPrice(amountInDollars / priceValue);

		return await orderService.PlaceOrderAsync(
			symbol,
			fractionalShares,
			Side.Buy,
			null,
			null,
			accountNumber,
			timeInForce,
			extendedHours,
			marketHours: marketHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderBuyLimitAsync(
		string symbol,
		int quantity,
		double limitPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Buy,
			limitPrice,
			null,
			accountNumber,
			timeInForce,
			extendedHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderBuyStopLossAsync(
		string symbol,
		int quantity,
		double stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Buy,
			null,
			stopPrice,
			accountNumber,
			timeInForce,
			extendedHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderBuyStopLimitAsync(
		string symbol,
		int quantity,
		double limitPrice,
		double stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Buy,
			limitPrice,
			stopPrice,
			accountNumber,
			timeInForce,
			extendedHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderSellMarketAsync(
		string symbol,
		int quantity,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Sell,
			null,
			null,
			accountNumber,
			timeInForce,
			extendedHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderSellFractionalByQuantityAsync(
		string symbol,
		int quantity,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false,
		string marketHours = "regular_hours")
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Sell,
			null,
			null,
			accountNumber,
			timeInForce,
			extendedHours,
			marketHours: marketHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderSellFractionalByPriceAsync(
		string symbol,
		double amountInDollars,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		if (amountInDollars < 1)
		{
			throw new HttpResponseException("Fractional share price should meet minimum 1.00");
		}

		_ = double.TryParse(await BidPriceAsync(symbol), out double priceValue);
		double fractionalShares = priceValue == 0 ? 0.0 : MathHelper.RoundPrice(amountInDollars / priceValue);

		return await orderService.PlaceOrderAsync(
			symbol,
			fractionalShares,
			Side.Sell,
			null,
			null,
			accountNumber,
			timeInForce,
			extendedHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderSellLimitAsync(
		string symbol,
		int quantity,
		double limitPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Sell,
			limitPrice,
			null,
			accountNumber,
			timeInForce,
			extendedHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderSellStopLossAsync(
		string symbol,
		int quantity,
		double stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Sell,
			null,
			stopPrice,
			accountNumber,
			timeInForce,
			extendedHours);
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderSellStopLimitAsync(
		string symbol,
		int quantity,
		double limitPrice,
		double stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false)
	{
		return await orderService.PlaceOrderAsync(
			symbol,
			quantity,
			Side.Sell,
			limitPrice,
			stopPrice,
			accountNumber,
			timeInForce,
			extendedHours);
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
