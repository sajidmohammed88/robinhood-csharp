namespace RobinhoodApi.Data.Base;

public class BaseHistoricalData
{
	public string Bounds { get; set; }

	public string Interval { get; set; }

	public string Span { get; set; }

	public string Symbol { get; set; }

	public string OpenPrice { get; set; }

	public DateTime OpenTime { get; set; }

	public string PreviousClosePrice { get; set; }

	public DateTime PreviousCloseTime { get; set; }
}
