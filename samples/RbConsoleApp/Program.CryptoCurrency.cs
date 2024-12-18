using Rb.Integration.Api.Data.Crypto;
using Rb.Integration.Api.Data.Crypto.Request;
using Rb.Integration.Api.Enum;
using Rb.Integration.Api.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp;

public static partial class Program
{
	public static async Task FetchCryptoDataAsync()
	{
		IList<CurrencyPair> currencyPairs = await _robinhood.GetCurrencyPairsAsync();

		Quotes quote = await _robinhood.GetQuotesAsync("LTCUSD");

		IList<CryptoAccount> accounts = await _robinhood.GetAccountsAsync();

		CryptoOrder order = await _robinhood.TradeAsync("DOGEUSD", new CryptoOrderRequest
		{
			Price = "1",
			Quantity = "1",
			Side = Side.Buy,
			TimeInForce = TimeInForce.Gtc,
			Type = OrderType.Market
		});

		IList<CryptoOrder> tradeHistory = await _robinhood.GetTradeHistoryAsync();

		try
		{
			CryptoOrder orderStatus = await _robinhood.GetOrderStatusAsync("67621b65-ce79-4f23-b06f-ed3652d80140");
		}
		catch (HttpResponseException ex)
		{
			// handle exception
			Console.WriteLine(ex.Message);
		}

		try
		{
			bool isOrderCanceled = await _robinhood.CancelCryptoOrderAsync("67621b65-ce79-4f23-b06f-ed3652d80140");
		}
		catch (HttpResponseException ex)
		{
			// handle exception
			Console.WriteLine(ex.Message);
		}

		CryptoHistoricalData historicalData = await _robinhood.HistoricalsAsync("BTCUSD", "5minute", "day", "24_7");

		IList<Holding> holdings = await _robinhood.HoldingsAsync();
	}
}
