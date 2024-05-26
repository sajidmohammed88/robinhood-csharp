using Rb.Integration.Api.Data.Crypto;
using Rb.Integration.Api.Data.Crypto.Request;
using Rb.Integration.Api.Enum;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp;

public static partial class Program
{
	public static async Task FetchCryptoData()
	{
		IList<CurrencyPair> currencyPairs = await _robinhood.GetCurrencyPairs();

		Quotes quote = await _robinhood.GetQuotes("LTCUSD");

		var accounts = await _robinhood.GetAccounts();

		CryptoOrder order = await _robinhood.Trade("LTCUSD", new CryptoOrderRequest
		{
			Type = OrderType.Market,
			Price = "1",
			Quantity = 1,
			Side = Side.Buy,
			TimeInForce = TimeInForce.Gtc
		});

		IList<CryptoOrder> tradeHistory = await _robinhood.GetTradeHistory();

		CryptoOrder orderStatus = await _robinhood.GetOrderStatus("60afb549-abce-40bf-bda9-50f0715f0903");

		bool isOrderCanceled = await _robinhood.CancelCryptoOrder("60afb549-abce-40bf-bda9-50f0715f0903");

		CryptoHistoricalData historicalData = await _robinhood.Historicals("BTCUSD", "5minute", "day", "24_7");

		IList<Holding> holdings = await _robinhood.Holdings();
	}
}
