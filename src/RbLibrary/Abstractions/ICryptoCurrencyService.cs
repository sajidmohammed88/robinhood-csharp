using Rb.Integration.Api.Data.Crypto;
using Rb.Integration.Api.Data.Crypto.Request;

namespace Rb.Integration.Api.Abstractions;

/// <summary>
/// Robinhood crypto currency service interface.
/// </summary>
public interface ICryptoCurrencyService
{
	/// <summary>
	/// Get the currency pairs.
	/// </summary>
	/// <returns>The currency pairs.</returns>
	Task<IList<CurrencyPair>> GetCurrencyPairsAsync();

	/// <summary>
	/// Get the crypto currency quotes.
	/// </summary>
	/// <param name="pair">The pair.</param>
	/// <returns>The quotes</returns>
	Task<Quotes> GetQuotesAsync(string pair);

	/// <summary>
	/// Get the crypto accounts.
	/// </summary>
	/// <returns>The accounts.</returns>
	Task<IList<CryptoAccount>> GetAccountsAsync();

	/// <summary>
	/// Trade the specified pair.
	/// </summary>
	/// <param name="pair">The pair.</param>
	/// <param name="orderRequest">The order request.</param>
	/// <returns>The crypto order.</returns>
	Task<CryptoOrder> TradeAsync(string pair, CryptoOrderRequest orderRequest);

	/// <summary>
	/// Gets the trade history.
	/// </summary>
	/// <returns>The crypto order list.</returns>
	Task<IList<CryptoOrder>> GetTradeHistoryAsync();

	/// <summary>
	/// Get the order status.
	/// </summary>
	/// <param name="orderId">The order identifier.</param>
	/// <returns>The crypto order.</returns>
	Task<CryptoOrder> GetOrderStatusAsync(string orderId);

	/// <summary>
	/// Cancel the crypto order.
	/// </summary>
	/// <param name="orderId">The order identifier.</param>
	/// <returns>true, if the order is canceled, otherwise false.</returns>
	Task<bool> CancelCryptoOrderAsync(string orderId);

	/// <summary>
	/// Historical for crypto order.
	/// </summary>
	/// <param name="pair">The pair.</param>
	/// <param name="interval">The interval.</param>
	/// <param name="span">The span.</param>
	/// <param name="bounds">The bounds.</param>
	/// <returns>Historical data.</returns>
	Task<CryptoHistoricalData> HistoricalsAsync(string pair, string interval, string span, string bounds);

	/// <summary>
	/// Get holdings.
	/// </summary>
	/// <returns>The holdings.</returns>
	Task<IList<Holding>> HoldingsAsync();
}
