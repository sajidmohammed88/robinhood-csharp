using Microsoft.Extensions.Diagnostics.Metrics;
using Rb.Integration.Api.Enum;

namespace Rb.Integration.Api.Data.Orders.Request;

public class OrderRequest : BaseOrderRequest
{
	public string Account { get; set; }

	public string AskPrice { get; set; }

	public string BidAskTimestamp { get; set; }

	public string BidPrice { get; set; }

	public string Instrument { get; set; }

	public string MarketHours { get; set; }

	public int? OrderFormVersion { get; set; }

	public string Symbol { get; set; }

	public Trigger? Trigger { get; set; }

	public string StopPrice { get; set; }

	public bool? ExtendedHours { get; set; }

	public string PresetPercentLimit { get; set; }
}
