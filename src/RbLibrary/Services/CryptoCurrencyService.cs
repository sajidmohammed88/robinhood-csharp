﻿using System.Text.Json;

namespace Rb.Integration.Api.Services;

/// <summary>
/// The robinhood crypto currency service.
/// </summary>
/// <seealso cref="ICryptoCurrencyService" />
public class CryptoCurrencyService(IHttpClientManager httpClientManager, IPaginator paginator) : ICryptoCurrencyService
{
	/// <inheritdoc />
	public async Task<IList<CurrencyPair>> GetCurrencyPairsAsync()
	{
		BaseResult<CurrencyPair> currencyPairResult = await httpClientManager.GetAsync<BaseResult<CurrencyPair>>(Constants.Routes.CurrencyPairs);

		return await paginator.PaginateResultAsync(currencyPairResult);
	}

	/// <inheritdoc />
	public async Task<Quotes> GetQuotesAsync(string pair)
	{
		if (string.IsNullOrEmpty(pair) || !Constants.Pairs.TryGetValue(pair, out string value))
		{
			throw new CryptoCurrencyException("The pair is null, empty or not exist.");
		}

		return await httpClientManager.GetAsync<Quotes>(string.Format(Constants.Routes.NummusQuotes, value));
	}

	/// <inheritdoc />
	public async Task<IList<CryptoAccount>> GetAccountsAsync()
	{
		BaseResult<CryptoAccount> cryptoAccountResult = await httpClientManager.GetAsync<BaseResult<CryptoAccount>>(Constants.Routes.NummusAccounts);

		if (cryptoAccountResult?.Results == null || !cryptoAccountResult.Results.Any() || cryptoAccountResult.Results.All(a => a.Status != "active"))
		{
			throw new HttpResponseException("The crypto currency account for the user need to be approved or disabled.");
		}

		return await paginator.PaginateResultAsync(cryptoAccountResult);
	}

	/// <inheritdoc />
	public async Task<CryptoOrder> TradeAsync(string pair, CryptoOrderRequest orderRequest)
	{
		if (string.IsNullOrEmpty(pair) || !Constants.Pairs.ContainsKey(pair))
		{
			throw new CryptoCurrencyException("The pair is null, empty or not exist.");
		}

		orderRequest.AccountId = (await GetAccountsAsync()).First().Id;
		orderRequest.CurrencyPairId = Constants.Pairs[pair];
		orderRequest.IsQuantityCollared = false;

		try
		{
			var rst = await httpClientManager.PostAsync<CryptoOrder>(Constants.Routes.NummusOrders, orderRequest);
			return rst.Data;
		}
		catch (Exception ex)
		{
			return new CryptoOrder
			{
				Detail = ex.Message
			};
		}
	}

	/// <inheritdoc />
	public async Task<IList<CryptoOrder>> GetTradeHistoryAsync()
	{
		BaseResult<CryptoOrder> cryptoOrderResult = await httpClientManager.GetAsync<BaseResult<CryptoOrder>>(Constants.Routes.NummusOrders);
		return await paginator.PaginateResultAsync(cryptoOrderResult);
	}

	/// <inheritdoc />
	public async Task<CryptoOrder> GetOrderStatusAsync(string orderId)
	{
		return await httpClientManager.GetAsync<CryptoOrder>(string.Format(Constants.Routes.OrderStatus, orderId));
	}

	/// <inheritdoc />
	public async Task<bool> CancelCryptoOrderAsync(string orderId)
	{
		CryptoOrder order = await GetOrderStatusAsync(orderId);
		if (order == null)
		{
			throw new HttpResponseException($"The order {orderId} not exist.");
		}

		if (order.CancelUrl == null)
		{
			throw new HttpResponseException($"The order {orderId} can't be canceled.");
		}

		(HttpStatusCode statusCode, string _) = await httpClientManager.PostAsync(order.CancelUrl, null, (null, null));

		return statusCode == HttpStatusCode.OK;
	}

	/// <inheritdoc />
	public async Task<CryptoHistoricalData> HistoricalsAsync(string pair, string interval, string span, string bounds)
	{
		if (string.IsNullOrEmpty(pair) || !Constants.Pairs.TryGetValue(pair, out string value))
		{
			throw new CryptoCurrencyException("The pair is null, empty or not exist.");
		}

		return await httpClientManager.GetAsync<CryptoHistoricalData>(
			string.Format(Constants.Routes.NummusHistoricals, value, interval, span, bounds));
	}

	/// <inheritdoc />
	public async Task<IList<Holding>> HoldingsAsync()
	{
		BaseResult<Holding> result = await httpClientManager.GetAsync<BaseResult<Holding>>(Constants.Routes.Holdings);

		return await paginator.PaginateResultAsync(result);
	}
}
