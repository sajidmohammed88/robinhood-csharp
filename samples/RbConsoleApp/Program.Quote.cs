using Rb.Integration.Api.Data.News;
using Rb.Integration.Api.Data.Quote;
using Rb.Integration.Api.Data.User;
using Rb.Integration.Api.Enum;
using Rb.Integration.Api.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp;

public static partial class Program
{
	public static async Task FetchQuoteDataAsync()
	{
		QuoteData quoteData = await _robinhood.GetQuoteDataAsync("AAPL");
		IList<QuoteData> quotesData = await _robinhood.GetQuotesDataAsync(["AAPL", "SNAP"]);

		IList<string> valuesByStock = await _robinhood.GetQuoteWithSpecifiedKeysAsync(
			"AAPL",
			"Symbol,LastTradePrice");

		Console.WriteLine($"{valuesByStock[0]}:{valuesByStock[1]}");

		IDictionary<string, IList<string>> valuesByStocks = await _robinhood.GetQuotesWithSpecifiedKeysAsync(
			["AAPL", "SNAP"],
			"Symbol,LastTradePrice");

		foreach ((string key, IList<string> value) in valuesByStocks)
		{
			Console.WriteLine($"stock :: {key} => {value[0]}:{value[1]}");
		}

		IList<HistoricalsData> historicalsData = await _robinhood.GetHistoricalQuotesAsync(
			["AAPL", "SNAP"],
			"5minute",
			Span.Day);

		IList<NewsData> news = await _robinhood.GetNewsAsync("AAPL");

		// to ask price, call common method GetQuoteWithSpecifiedKeysAsync and use the same thing for all other keys like : ask_size,bid_price etc.
		string askPrice = await _robinhood.AskPriceAsync("AAPL");
		string bidPrice = await _robinhood.BidPriceAsync("AAPL");

		IList<string> tickers = await _robinhood.GetTickersByTagAsync("top-movers");

		Account account = await _robinhood.GetAccountAsync();
		IList<Instrument> watchList = await _robinhood.GetWatchListsAsync();

		// not worked routes:
		try
		{
			dynamic marketData = await _robinhood.GetStockMarketDataAsync(
				[
					"450dfc6d-5510-4d40-abfb-f633b7d9be3e",
					"1e513292-5926-4dc4-8c3d-4af6b5836704"
				]);
		}
		catch (HttpResponseException)
		{
			// handle exception
			Console.WriteLine("API GetStockMarketDataAsync not working");
		}

		try
		{
			dynamic popularity = await _robinhood.GetPopularityAsync("F");
		}
		catch (HttpResponseException)
		{
			// handle exception
			Console.WriteLine("API GetPopularityAsync not working");
		}
	}
}
