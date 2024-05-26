using Rb.Integration.Api.Abstractions;
using Rb.Integration.Api.Data.Base;
using Rb.Integration.Api.Data.Crypto;
using Rb.Integration.Api.Data.Crypto.Request;
using Rb.Integration.Api.Exceptions;

using System.Net;
using System.Text.Json;

namespace Rb.Integration.Api.Services;

/// <summary>
/// The robinhood crypto currency service.
/// </summary>
/// <seealso cref="ICryptoCurrencyService" />
public class CryptoCurrencyService(IHttpClientManager httpClientManager, IPaginator paginator) : ICryptoCurrencyService
{
	/// <inheritdoc />
	public async Task<IList<CurrencyPair>> GetCurrencyPairs()
	{
		BaseResult<CurrencyPair> currencyPairResult = await httpClientManager.GetAsync<BaseResult<CurrencyPair>>(Constants.Routes.CurrencyPairs);

		return await paginator.PaginateResultAsync(currencyPairResult);
	}

	/// <inheritdoc />
	public async Task<Quotes> GetQuotes(string pair)
	{
		if (string.IsNullOrEmpty(pair) || !Constants.Pairs.TryGetValue(pair, out string value))
		{
			throw new CryptoCurrencyException("The pair is null, empty or not exist.");
		}

		return await httpClientManager.GetAsync<Quotes>(string.Format(Constants.Routes.NummusQuotes, value));
	}

	/// <inheritdoc />
	public async Task<IList<CryptoAccount>> GetAccounts()
	{
		BaseResult<CryptoAccount> cryptoAccountResult = await httpClientManager.GetAsync<BaseResult<CryptoAccount>>(Constants.Routes.NummusAccounts);

		if (cryptoAccountResult?.Results == null || !cryptoAccountResult.Results.Any() || cryptoAccountResult.Results.All(a => a.Status != "active"))
		{
			throw new HttpResponseException("The crypto currency account for the user need to be approved or disabled.");
		}

		return await paginator.PaginateResultAsync(cryptoAccountResult);
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
			return await httpClientManager.PostJsonAsync<CryptoOrder>(Constants.Routes.NummusOrders, JsonSerializer.Serialize(orderRequest, CustomJsonSerializerOptions.Current));
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
		BaseResult<CryptoOrder> cryptoOrderResult = await httpClientManager.GetAsync<BaseResult<CryptoOrder>>(Constants.Routes.NummusOrders);
		return await paginator.PaginateResultAsync(cryptoOrderResult);
	}

	/// <inheritdoc />
	public async Task<CryptoOrder> GetOrderStatus(string orderId)
	{
		return await httpClientManager.GetAsync<CryptoOrder>(string.Format(Constants.Routes.OrderStatus, orderId));
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

		var (statusCode, _) = await httpClientManager.PostAsync(order.CancelUrl, null, (null, null));

		return statusCode == HttpStatusCode.OK;
	}

	/// <inheritdoc />
	public async Task<CryptoHistoricalData> Historicals(string pair, string interval, string span, string bounds)
	{
		if (string.IsNullOrEmpty(pair) || !Constants.Pairs.ContainsKey(pair))
		{
			throw new CryptoCurrencyException("The pair is null, empty or not exist.");
		}

		return await httpClientManager.GetAsync<CryptoHistoricalData>(
			string.Format(Constants.Routes.NummusHistoricals, Constants.Pairs[pair], interval, span, bounds));
	}

	/// <inheritdoc />
	public async Task<IList<Holding>> Holdings()
	{
		BaseResult<Holding> result = await httpClientManager.GetAsync<BaseResult<Holding>>(Constants.Routes.Holdings);

		return await paginator.PaginateResultAsync(result);
	}
}
