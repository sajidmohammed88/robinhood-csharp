using RobinhoodLibrary.Data.Crypto;
using RobinhoodLibrary.Data.Crypto.Request;
using RobinhoodLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp
{
    public partial class Program
    {
        public static async Task FetchCryptoData()
        {
            IList<CurrencyPair> currencyPairs = await _robinhood.GetCurrencyPairs();

            Quotes quote = await _robinhood.GetQuotes("BTCUSD");

            var accounts = await _robinhood.GetAccounts();

            CryptoOrder order = await _robinhood.Trade("BTCUSD", new CryptoOrderRequest
            {
                OrderType = OrderType.Market,
                Price = (Math.Round(float.Parse(quote.MarkPrice, CultureInfo.InvariantCulture) * 1.005, 3)).ToString(CultureInfo.InvariantCulture),
                Quantity = "0.00005",
                Side = Side.Buy,
                TimeInForce = TimeInForce.Gfd
            });

            IList<CryptoOrder> tradeHistory = await _robinhood.GetTradeHistory();

            CryptoOrder orderStatus = await _robinhood.GetOrderStatus("");

            bool isOrderCanceled = await _robinhood.CancelCryptoOrder("");

            CryptoHistoricalData historicalData = await _robinhood.Historicals("BTCUSD", "5minute", "day", "24_7");

            IList<Holding> holdings = await _robinhood.Holdings();
        }
    }
}
