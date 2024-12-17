namespace Rb.Integration.Api.Data.Quote;

public class HistoricalsData : BaseHistoricalData
{
	public string Quote { get; set; } // Example: "https://api.robinhood.com/quotes/450dfc6d-5510-4d40-abfb-f633b7d9be3e/"

	public string Instrument { get; set; } // Example: "https://api.robinhood.com/instruments/450dfc6d-5510-4d40-abfb-f633b7d9be3e/"

	[JsonPropertyName("InstrumentID")]
	public string InstrumentId { get; set; } // Example: "450dfc6d-5510-4d40-abfb-f633b7d9be3e"

	public IList<Historical> Historicals { get; set; }
}
