namespace Rb.Integration.Api.Data.Base;

public class BaseHistoricalData
{
	public string Bounds { get; set; } // Example: "24_7"

	public string Interval { get; set; } // Example: "5minute"

	public string Span { get; set; } // Example: day

	public string Symbol { get; set; } // Example: "AAPL"

	public string OpenPrice { get; set; }

	public DateTime? OpenTime { get; set; }

	public string PreviousClosePrice { get; set; }

	public DateTime? PreviousCloseTime { get; set; }
}
