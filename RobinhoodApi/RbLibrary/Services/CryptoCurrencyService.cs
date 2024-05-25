using RobinhoodApi.Abstractions;
using RobinhoodApi.Data.Base;
using RobinhoodApi.Data.Crypto;
using RobinhoodApi.Data.Crypto.Request;
using RobinhoodApi.Exceptions;

using System.Net;
using System.Text.Json;

namespace RobinhoodApi.Services;

/// <summary>
/// The robinhood crypto currency service.
/// </summary>
/// <seealso cref="ICryptoCurrencyService" />
public class CryptoCurrencyService : ICryptoCurrencyService
{
	private readonly IHttpClientManager _httpClientManager;
	private readonly IPaginator _paginator;
	private readonly JsonSerializerOptions _settings;

	public CryptoCurrencyService(IHttpClientManager httpClientManager, IPaginator paginator)
	{
		_httpClientManager = httpClientManager;
		_paginator = paginator;
		_settings = CustomJsonSerializerOptions.Current;
	}

	/// <inheritdoc />
	public async Task<IList<CurrencyPair>> GetCurrencyPairs()
	{
		BaseResult<CurrencyPair> currencyPairResult = await _httpClientManager.GetAsync<BaseResult<CurrencyPair>>(Constants.Routes.CurrencyPairs);

		return await _paginator.PaginateResultAsync(currencyPairResult);
	}

	/// <inheritdoc />
	public async Task<Quotes> GetQuotes(string pair)
	{
		if (string.IsNullOrEmpty(pair) || !Constants.Pairs.ContainsKey(pair))
		{
			throw new CryptoCurrencyException("The pair is null, empty or not exist.");
		}

		return await _httpClientManager.GetAsync<Quotes>(string.Format(Constants.Routes.NummusQuotes, Constants.Pairs[pair]));
	}

	/// <inheritdoc />
	public async Task<IList<CryptoAccount>> GetAccounts()
	{
		BaseResult<CryptoAccount> cryptoAccountResult = await _httpClientManager.GetAsync<BaseResult<CryptoAccount>>(Constants.Routes.NummusAccounts);

		if (cryptoAccountResult?.Results == null || !cryptoAccountResult.Results.Any() || cryptoAccountResult.Results.All(a => a.Status != "active"))
		{
			throw new HttpResponseException("The crypto currency account for the user need to be approved or disabled.");
		}

		return await _paginator.PaginateResultAsync(cryptoAccountResult);
	}

	/// <inheritdoc />
	public async Task<CryptoOrder> Trade(string pair, CryptoOrderRequest orderRequest)
	{
		if (string.IsNullOrEmpty(pair) || !Constants.Pairs.ContainsKey(pair))
		{
			throw new CryptoCurrencyException("The pair is null, empty or not exist.");
		}

		orderRequest.AccountId = (await GetAccounts()).First().Id;
		orderRequest.CurrencyPairId = Constants.Pairs[pair];

		try
		{
			return await _httpClientManager.PostJsonAsync<CryptoOrder>(Constants.Routes.NummusOrders, JsonSerializer.Serialize(orderRequest, _settings));
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
	public async Task<IList<CryptoOrder>> GetTradeHistory()
	{
		BaseResult<CryptoOrder> cryptoOrderResult = await _httpClientManager.GetAsync<BaseResult<CryptoOrder>>(Constants.Routes.NummusOrders);
		return await _paginator.PaginateResultAsync(cryptoOrderResult);
	}

	/// <inheritdoc />
	public async Task<CryptoOrder> GetOrderStatus(string orderId)
	{
		return await _httpClientManager.GetAsync<CryptoOrder>(string.Format(Constants.Routes.OrderStatus, orderId));
	}

	/// <inheritdoc />
	public async Task<bool> CancelCryptoOrder(string orderId)
	{
		CryptoOrder order = await GetOrderStatus(orderId);
		if (order == null)
		{
			throw new HttpResponseException($"The order {orderId} not exist.");
		}

		if (order.CancelUrl == null)
		{
			throw new HttpResponseException($"The order {orderId} can't be canceled.");
		}

		var (statusCode, _) = await _httpClientManager.PostAsync(order.CancelUrl, null, (null, null));

		return statusCode == HttpStatusCode.OK;
	}

	/// <inheritdoc />
	public async Task<CryptoHistoricalData> Historicals(string pair, string interval, string span, string bounds)
	{
		if (string.IsNullOrEmpty(pair) || !Constants.Pairs.ContainsKey(pair))
		{
			throw new CryptoCurrencyException("The pair is null, empty or not exist.");
		}

		return await _httpClientManager.GetAsync<CryptoHistoricalData>(
			string.Format(Constants.Routes.NummusHistoricals, Constants.Pairs[pair], interval, span, bounds));
	}

	/// <inheritdoc />
	public async Task<IList<Holding>> Holdings()
	{
		BaseResult<Holding> result = await _httpClientManager.GetAsync<BaseResult<Holding>>(Constants.Routes.Holdings);

		return await _paginator.PaginateResultAsync(result);
	}
}
