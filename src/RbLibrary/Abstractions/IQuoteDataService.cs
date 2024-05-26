using Rb.Integration.Api.Data.News;
using Rb.Integration.Api.Data.Quote;
using Rb.Integration.Api.Data.User;
using Rb.Integration.Api.Enum;

namespace Rb.Integration.Api.Abstractions;

/// <summary>
/// Quote interface that responsible on quote data endpoints.
/// </summary>
public interface IQuoteDataService
{
	/// <summary>
	/// Get the quote data.
	/// </summary>
	/// <param name="stock">The stock instrument or symbol.</param>
	/// <returns>The quote data</returns>
	Task<QuoteData> GetQuoteDataAsync(string stock);

	/// <summary>
	/// Get the quote with a specified keys.
	/// </summary>
	/// <param name="stock">The stock.</param>
	/// <param name="keys">The attributes to fetch.</param>
	/// <returns>The value of specified keys.</returns>
	Task<IList<string>> GetQuoteWithSpecifiedKeysAsync(string stock, string keys);

	/// <summary>
	/// Get the quotes data.
	/// </summary>
	/// <param name="stocks">The stock list.</param>
	/// <returns>The quotes data</returns>
	Task<IList<QuoteData>> GetQuotesDataAsync(IList<string> stocks);

	/// <summary>
	/// Get the quotes with specified keys.
	/// </summary>
	/// <param name="stocks">The stocks.</param>
	/// <param name="keys">The attributes to fetch.</param>
	/// <returns>he value of specified keys by stock.</returns>
	Task<IDictionary<string, IList<string>>> GetQuotesWithSpecifiedKeysAsync(IList<string> stocks, string keys);

	/// <summary>
	/// Get the historical data of stock.
	/// </summary>
	/// <param name="stocks">The stock tickers.</param>
	/// <param name="interval">The resolution of data.</param>
	/// <param name="span">The span.</param>
	/// <param name="bounds">The bounds.</param>
	/// <returns>Historical data of stock.</returns>
	Task<IList<HistoricalsData>> GetHistoricalQuotesAsync(IList<string> stocks, string interval, Span span, Bounds bounds = Bounds.Regular);

	/// <summary>
	/// Gets the tickers by tag.
	/// </summary>
	/// <param name="tag">The tag.</param>
	/// <returns>The tickers.</returns>
	Task<IList<string>> GetTickersByTagAsync(string tag);

	/// <summary>
	/// Get the instrument.
	/// </summary>
	/// <param name="url">The URL.</param>
	/// <returns>The instrument.</returns>
	Task<Instrument> GetInstrumentAsync(string url);

	/// <summary>
	/// Get the news.
	/// </summary>
	/// <param name="stock">The stock.</param>
	/// <returns>The news data list.</returns>
	Task<IList<NewsData>> GetNewsAsync(string stock);

	/// <summary>
	/// Get the account.
	/// </summary>
	/// <returns>The account information</returns>
	Task<Account> GetAccountAsync();

	/// <summary>
	/// Gets the instrument watch lists.
	/// </summary>
	/// <returns>The instrument list.</returns>
	Task<IList<Instrument>> GetWatchListsAsync();

	/// <summary>
	/// Get the stock market data.
	/// </summary>
	/// <param name="instruments">The instruments.</param>
	/// <returns>Stock market data.</returns>
	Task<dynamic> GetStockMarketDataAsync(IList<string> instruments);

	/// <summary>
	/// Get the number of robinhood users who own the given stock.
	/// </summary>
	/// <param name="stock">The stock.</param>
	/// <returns>The number of robinhood users who own the given stock</returns>
	Task<dynamic> GetPopularityAsync(string stock);
}
