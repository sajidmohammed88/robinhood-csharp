using RobinhoodApi.Data.News;
using RobinhoodApi.Data.Quote;
using RobinhoodApi.Data.User;
using RobinhoodApi.Enum;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp;

public partial class Program
{
	public static async Task FetchQuoteData()
	{
		QuoteData quoteData = await _robinhood.GetQuoteData("AAPL");
		IList<QuoteData> quotesData = await _robinhood.GetQuotesData(["AAPL", "SNAP"]);

		IList<string> valuesByStock = await _robinhood.GetQuoteWithSpecifiedKeys("AAPL", "symbol,last_trade_price");
		Console.WriteLine($"{valuesByStock[0]}:{valuesByStock[1]}");

		IDictionary<string, IList<string>> valuesByStocks = await _robinhood
			.GetQuotesWithSpecifiedKeys(["AAPL", "SNAP"], "symbol,last_trade_price")
			;
		foreach ((string key, IList<string> value) in valuesByStocks)
		{
			Console.WriteLine($"stock :: {key} => {value[0]}:{value[1]}");
		}

		IList<HistoricalsData> historicalsData = await _robinhood
			.GetHistoricalQuotes(["AAPL", "SNAP"], "5minute", Span.Day);

		IList<NewsData> news = await _robinhood.GetNews("AAPL");

		// to ask price, call common method GetQuoteWithSpecifiedKeysAsync and use the same thing for all other keys like : ask_size,bid_price etc.
		string askPrice = await _robinhood.AskPrice("AAPL");
		string bidPrice = await _robinhood.BidPrice("AAPL");

		IList<string> tickers = await _robinhood.GetTickersByTag("top-movers");

		Account account = await _robinhood.GetAccount();
		IList<Instrument> watchList = await _robinhood.GetWatchLists();

		// not worked routes:
		dynamic marketData = await _robinhood.GetStockMarketData(
		[
			"450dfc6d-5510-4d40-abfb-f633b7d9be3e",
			"1e513292-5926-4dc4-8c3d-4af6b5836704"
		]);

		dynamic popularity = await _robinhood.GetPopularity("F");
	}
}
