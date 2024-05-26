using Rb.Integration.Api.Data.Crypto;
using Rb.Integration.Api.Data.Crypto.Request;
using Rb.Integration.Api.Enum;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp;

public static partial class Program
{
	public static async Task FetchCryptoDataAsync()
	{
		IList<CurrencyPair> currencyPairs = await _robinhood.GetCurrencyPairsAsync();

		Quotes quote = await _robinhood.GetQuotesAsync("LTCUSD");

		var accounts = await _robinhood.GetAccountsAsync();

		CryptoOrder order = await _robinhood.TradeAsync("LTCUSD", new CryptoOrderRequest
		{
			Type = OrderType.Market,
			Price = "1",
			Quantity = 1,
			Side = Side.Buy,
			TimeInForce = TimeInForce.Gtc
		});

		IList<CryptoOrder> tradeHistory = await _robinhood.GetTradeHistoryAsync();

		CryptoOrder orderStatus = await _robinhood.GetOrderStatusAsync("60afb549-abce-40bf-bda9-50f0715f0903");

		bool isOrderCanceled = await _robinhood.CancelCryptoOrderAsync("60afb549-abce-40bf-bda9-50f0715f0903");

		CryptoHistoricalData historicalData = await _robinhood.HistoricalsAsync("BTCUSD", "5minute", "day", "24_7");

		IList<Holding> holdings = await _robinhood.HoldingsAsync();
	}
}
