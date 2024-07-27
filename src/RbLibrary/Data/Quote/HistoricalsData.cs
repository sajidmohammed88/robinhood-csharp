namespace Rb.Integration.Api.Data.Quote;

public class HistoricalsData : BaseHistoricalData
{
	public string Quote { get; set; }

	public string Instrument { get; set; }

	[JsonPropertyName("InstrumentID")]
	public string InstrumentId { get; set; }

	public IList<Historical> Historicals { get; set; }
}
