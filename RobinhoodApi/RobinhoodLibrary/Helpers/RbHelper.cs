using RobinhoodLibrary.Data.Crypto.Request;
using RobinhoodLibrary.Data.Orders.Request;
using RobinhoodLibrary.Data.Quote;
using RobinhoodLibrary.Enum;
using RobinhoodLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Helpers
{
    /// <summary>
    /// Class used to help build static functions.
    /// </summary>
    internal static class RbHelper
    {
        private static string GetValue(object obj, string key) =>
            obj.GetType().GetProperties()
                .FirstOrDefault(_ => _.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name == key)
                ?.GetValue(obj)
                ?.ToString();

        internal static IList<string> GetValueByKeys(string keys, QuoteData quoteData)
        {
            return keys.Split(',')
                .Select(key => GetValue(quoteData, key))
                .ToList();
        }

        internal static string BuildUrlMarketData(string optionId = null) => !string.IsNullOrEmpty(optionId)
            ? $"{Constants.Routes.MarketDataBase}{optionId}/"
            : Constants.Routes.MarketDataBase;

        internal static IDictionary<string, string> BuildOrderContent(OrderRequest orderRequest, string accountUrl) =>
            new Dictionary<string, string>
            {
                {"account", accountUrl},
                {"instrument", orderRequest.InstrumentUrl},
                {"symbol", orderRequest.Symbol},
                {"type", orderRequest.OrderType.ToString().ToLower()},
                {"time_in_force", orderRequest.TimeInForce.ToString().ToLower()},
                {"trigger", orderRequest.Trigger.ToString().ToLower()},
                {"price", orderRequest.Price},
                {"stop_price", orderRequest.StopPrice},
                {"quantity", orderRequest.Quantity.ToString()},
                {"side", orderRequest.Side.ToString().ToLower()}
            };

        internal static IDictionary<string, string> BuildTradeContent(CryptoOrderRequest orderRequest, string accountId, string pair) =>
            new Dictionary<string, string>
            {
                {"account_id", accountId},
                {"currency_pair_id", Pairs[pair]},
                {"ref_id", Guid.NewGuid().ToString()},
                {"price", orderRequest.Price},
                {"quantity", orderRequest.Quantity},
                {"side", orderRequest.Side.ToString().ToLower()},
                {"time_in_force", orderRequest.TimeInForce.ToString().ToLower()},
                {"type", orderRequest.OrderType.ToString().ToLower()},
            };

        internal static OrderRequest BuildOrderRequestForMarket(string instrumentUrl, string symbol,
            TimeInForce timeInForce, int quantity, OrderType orderType, Trigger trigger, Side side, string price = null,
            string stopPrice = null) =>
            new OrderRequest
            {
                InstrumentUrl = instrumentUrl,
                Symbol = symbol,
                TimeInForce = timeInForce,
                Quantity = quantity,
                Price = price,
                StopPrice = stopPrice,
                OrderType = orderType,
                Trigger = trigger,
                Side = side
            };

        internal static void CheckOrderRequest(OrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                throw new RequestCheckException("The order request is null");
            }

            if (string.IsNullOrEmpty(orderRequest.InstrumentUrl))
            {
                if (string.IsNullOrEmpty(orderRequest.Symbol))
                {
                    throw new RequestCheckException("Neither instrumentURL nor symbol were passed.");
                }

                throw new RequestCheckException("InstrumentUrl is empty.");
            }

            if (string.IsNullOrEmpty(orderRequest.Symbol))
            {
                throw new RequestCheckException("Symbol is empty.");
            }

            if (orderRequest.OrderType == OrderType.Limit)
            {
                if (orderRequest.Price == null)
                {
                    throw new RequestCheckException("Limit order has no price.");
                }
            }

            if (orderRequest.Trigger == Trigger.Stop)
            {
                if (orderRequest.StopPrice == null)
                {
                    throw new RequestCheckException("Stop order don't have stop_price.");
                }
            }

            if (orderRequest.StopPrice != null && orderRequest.Trigger != Trigger.Stop)
            {
                throw new RequestCheckException("Stop price set for non-stop order.");
            }

            if (orderRequest.Price != null)
            {
                if (orderRequest.OrderType == OrderType.Market)
                {
                    throw new RequestCheckException("Market order has price limit.");
                }
            }

            if (orderRequest.Quantity <= 0)
            {
                throw new RequestCheckException("Quantity must be positive number.");
            }
        }

        internal static IDictionary<string, string> Pairs = new Dictionary<string, string>
        {
            {"BTCUSD", "3d961844-d360-45fc-989b-f6fca761d511"},
            {"ETHUSD", "76637d50-c702-4ed1-bcb5-5b0732a81f48"},
            {"ETCUSD", "7b577ce3-489d-4269-9408-796a0d1abb3a"},
            {"BCHUSD", "2f2b77c4-e426-4271-ae49-18d5cb296d3a"},
            {"BSVUSD", "086a8f9f-6c39-43fa-ac9f-57952f4a1ba6"},
            {"LTCUSD", "383280b1-ff53-43fc-9c84-f01afd0989cd"},
            {"DOGEUSD", "1ef78e1b-049b-4f12-90e5-555dcf2fe204"}
        };
    }
}
