namespace Rb.Integration.Api.Helpers;

/// <summary>
/// Class used to help build static functions.
/// </summary>
internal static class RbHelper
{
	private static string GetValue(object obj, string key)
	{
		return obj.GetType().GetProperties()
			.FirstOrDefault(_ => _.Name == key)
			?.GetValue(obj)
			?.ToString();
	}

	internal static IList<string> GetValueByKeys(string keys, QuoteData quoteData)
	{
		return keys.Split(',')
			.Select(key => GetValue(quoteData, key))
			.ToList();
	}

	internal static string BuildUrlMarketData(string optionId = null)
	{
		return !string.IsNullOrEmpty(optionId)
		? $"{Constants.Routes.MarketDataBase}{optionId}/"
		: Constants.Routes.MarketDataBase;
	}

	internal static IDictionary<string, string> BuildOrderContent(OrderRequest orderRequest, string accountUrl)
	{
		return new Dictionary<string, string>
		{
			{"account", accountUrl},
			{"instrument", orderRequest.InstrumentUrl},
			{"symbol", orderRequest.Symbol},
			{"type", orderRequest.Type.ToString().ToLower()},
			{"time_in_force", orderRequest.TimeInForce.ToString().ToLower()},
			{"trigger", orderRequest.Trigger.ToString().ToLower()},
			{"price", orderRequest.Price},
			{"stop_price", orderRequest.StopPrice},
			{"quantity", orderRequest.Quantity.ToString()},
			{"side", orderRequest.Side.ToString().ToLower()}
		};
	}

	internal static OrderRequest BuildOrderRequestForMarket(string instrumentUrl,
														 string symbol,
														 TimeInForce timeInForce,
														 int quantity,
														 OrderType orderType,
														 Trigger trigger,
														 Side side,
														 string price = null,
														 string stopPrice = null)
	{
		return new()
		{
			InstrumentUrl = instrumentUrl,
			Symbol = symbol,
			TimeInForce = timeInForce,
			Quantity = quantity,
			Price = price,
			StopPrice = stopPrice,
			Type = orderType,
			Trigger = trigger,
			Side = side
		};
	}

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

		if (orderRequest.Type == OrderType.Limit && orderRequest.Price is null)
		{
			throw new RequestCheckException("Limit order has no price.");
		}

		if (orderRequest.Trigger == Trigger.Stop && orderRequest.StopPrice is null)
		{
			throw new RequestCheckException("Stop order don't have stop_price.");
		}

		if (orderRequest.StopPrice != null && orderRequest.Trigger != Trigger.Stop)
		{
			throw new RequestCheckException("Stop price set for non-stop order.");
		}

		if (orderRequest.Price != null && orderRequest.Type == OrderType.Market)
		{
			throw new RequestCheckException("Market order has price limit.");
		}

		if (orderRequest.Quantity <= 0)
		{
			throw new RequestCheckException("Quantity must be positive number.");
		}
	}
}
