﻿namespace Rb.Integration.Api.Services;

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
}
