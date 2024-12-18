
using Rb.Integration.Api.Abstractions;
using Rb.Integration.Api.Data.Orders.Request;

namespace Rb.Integration.Api.Services;

/// <summary>
/// Order service that responsible on orders endpoints call.
/// </summary>
/// <seealso cref="IOrderService" />
public class OrderService(IQuoteDataService quoteDataService, IHttpClientManager httpClientManager, IPaginator paginator) : IOrderService
{
	/// <inheritdoc />
	public async Task<IList<Order>> GetOrdersHistoryAsync()
	{
		BaseResult<Order> orderResult = await httpClientManager.GetAsync<BaseResult<Order>>(Constants.Routes.Orders);

		return await paginator.PaginateResultAsync(orderResult);
	}

	/// <inheritdoc />
	public async Task<Order> GetOrderHistoryAsync(Guid orderId)
	{
		if (orderId == Guid.Empty)
		{
			throw new HttpResponseException("The order identifier is empty.");
		}

		return await httpClientManager.GetAsync<Order>($"{Constants.Routes.Orders}{orderId}/");
	}

	/// <inheritdoc />
	public async Task<Order> SubmitBuyOrderAsync(OrderRequest orderRequest)
	{
		return await SubmitOrder(orderRequest, true);
	}

	/// <inheritdoc />
	public async Task<Order> SubmitSellOrderAsync(OrderRequest orderRequest)
	{
		return await SubmitOrder(orderRequest, false);
	}

	/// <summary>
	/// Submits the order for sell or buy..
	/// </summary>
	/// <param name="orderRequest">The order request.</param>
	/// <param name="isBuy">if set to <c>true</c> is buy,otherwise false.</param>
	/// <returns>The order.</returns>
	/// <exception cref="HttpRequestException">Exception when placing an order, reason : {ex.Message}</exception>
	private async Task<Order> SubmitOrder(OrderRequest orderRequest, bool isBuy)
	{
		RbHelper.CheckOrderRequest(orderRequest);
		Account account = await quoteDataService.GetAccountAsync();
		if (orderRequest.Price == null)
		{
			QuoteData quote = await quoteDataService.GetQuoteDataAsync(orderRequest.Symbol);
			orderRequest.Price = (isBuy ? quote.AskPrice : quote.BidPrice) ?? quote.LastTradePrice;
		}

		try
		{
			var submitResult = await httpClientManager.PostAsync<Order>(Constants.Routes.Orders,
				RbHelper.BuildOrderContent(orderRequest, account.Url));
			return submitResult.Data;
		}
		catch (Exception ex)
		{
			throw new HttpRequestException($"Exception when placing an order, reason : {ex.Message}");
		}
	}

	/// <inheritdoc />
	public async Task<Order> PlaceOrderAsync(OrderRequest orderRequest)
	{
		if (orderRequest == null)
		{
			throw new RequestCheckException("The order request is null in call to PlaceOrder");
		}

		Account account = await quoteDataService.GetAccountAsync();
		if (orderRequest.Price is null or "0.0")
		{
			QuoteData quote = await quoteDataService.GetQuoteDataAsync(orderRequest.Symbol);
			orderRequest.Price = quote.BidPrice;
			if (quote.BidPrice is null or "0.0")
			{
				orderRequest.Price = quote.LastTradePrice;
			}
		}

		try
		{
			var response = await httpClientManager.PostAsync<Order>(Constants.Routes.Orders,
					RbHelper.BuildOrderContent(orderRequest, account.Url));
			return response.Data;
		}
		catch (Exception ex)
		{
			throw new HttpRequestException($"Exception when placing an order, reason : {ex.Message}");
		}
	}

	public async Task<Order> PlaceOrderAsync(
		string symbol,
		double quantity,
		Side side,
		double? limitPrice,
		double? stopPrice,
		string accountNumber = null,
		TimeInForce timeInForce = TimeInForce.Gfd,
		bool extendedHours = false,
		string marketHours = "regular_hours")
	{
		Account account = await quoteDataService.GetAccountAsync();
		QuoteData quoteData = await quoteDataService.GetQuoteDataAsync(symbol);
		string accountUrl = account.Url;
		string instrumentUrl = quoteData.Instrument;
		string askPrice = quoteData.AskPrice;
		string bidPrice = quoteData.BidPrice;

		OrderType orderType = OrderType.Market;
		Trigger trigger = Trigger.Immediate;

		double? price = null;
		string presetPercentLimit = null;

		if (limitPrice.HasValue && stopPrice.HasValue)
		{
			// Buy Stop Limit / Sell Stop Limit
			price = MathHelper.RoundPrice(limitPrice.Value);
			stopPrice = MathHelper.RoundPrice(stopPrice.Value);
			orderType = OrderType.Limit;
			trigger = Trigger.Stop;
		}
		else if (limitPrice.HasValue)
		{
			// Buy Limit / Sell Limit
			price = MathHelper.RoundPrice(limitPrice.Value);
			trigger = Trigger.Immediate;
			orderType = OrderType.Limit;
		}
		else if (stopPrice.HasValue)
		{
			// Buy Stop Loss / Sell Stop Loss
			stopPrice = MathHelper.RoundPrice(stopPrice.Value);
			trigger = Trigger.Stop;
			orderType = OrderType.Market;
		}
		else
		{
			// Buy / Sell
			trigger = Trigger.Immediate;

			if (side == Side.Buy)
			{
				presetPercentLimit = "0.05";

				double askPriceVal = double.Parse(askPrice);
				double presetPercentLimitVal = double.Parse(presetPercentLimit);
				price = MathHelper.RoundPrice(askPriceVal * (1.0 + presetPercentLimitVal));

				orderType = OrderType.Limit;
			}
			else
			{
				orderType = OrderType.Market;
			}
		}

		OrderRequest orderRequest = new OrderRequest
		{
			Account = accountUrl,
			AskPrice = askPrice,
			BidAskTimestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
			BidPrice = bidPrice,
			Instrument = instrumentUrl,
			MarketHours = marketHours,
			OrderFormVersion = 4,
			Price = price?.ToString(),
			Quantity = quantity.ToString(),
			Side = side,
			Symbol = symbol,
			TimeInForce = timeInForce,
			Trigger = trigger,
			Type = orderType,
			StopPrice = stopPrice?.ToString(),
			ExtendedHours = extendedHours,
			PresetPercentLimit = presetPercentLimit
		};

		// TODO: Only verified "regular_hours", not verified "extended_hours", "all_day_hours" yet;

		try
		{
			string orderUrl = GetOrderUrl(accountNumber: accountNumber);
			var rst = await httpClientManager.PostAsync<Order>(orderUrl, orderRequest);

			return rst.Data;
		}
		catch (Exception ex)
		{
			throw new HttpRequestException($"Exception when placing an order, reason : {ex.Message}");
		}
	}

	private string GetOrderUrl(string orderId = null, string accountNumber = null)
	{
		string url = Constants.Routes.Orders;
		if (!string.IsNullOrEmpty(orderId))
		{
			url += $"{orderId}/";
		}

		if (!string.IsNullOrEmpty(accountNumber))
		{
			url += $"?account_numbers={accountNumber}";
		}

		return url;
	}
}
