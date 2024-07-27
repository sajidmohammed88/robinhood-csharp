namespace Rb.Integration.Api.Data.Fundamentals;

public class Fundamental
{
	public string Open { get; set; }

	public string High { get; set; }

	public string Low { get; set; }

	public string Volume { get; set; }

	public string MarketDate { get; set; }

	[JsonPropertyName("average_volume_2_weeks")]
	public string AverageVolume2Weeks { get; set; }

	public string AverageVolume { get; set; }

	[JsonPropertyName("high_52_weeks")]
	public string High52Weeks { get; set; }

	public string DividendYield { get; set; }

	public string Float { get; set; }

	[JsonPropertyName("low_52_weeks")]
	public string Low52Weeks { get; set; }

	public string MarketCap { get; set; }

	public string PbRatio { get; set; }

	public string PeRatio { get; set; }

	public string SharesOutstanding { get; set; }

	public string Description { get; set; }

	public string Instrument { get; set; }

	public string Ceo { get; set; }

	public string HeadquartersCity { get; set; }

	public string HeadquartersState { get; set; }

	public string Sector { get; set; }

	public string Industry { get; set; }

	public int NumEmployees { get; set; }

	public int YearFounded { get; set; }
}
