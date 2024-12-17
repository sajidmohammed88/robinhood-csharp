namespace Rb.Integration.Api.Data.Quote;

public class QuoteData
{
	public string AskPrice { get; set; }

	public int? AskSize { get; set; }

	public string BidPrice { get; set; }

	public int? BidSize { get; set; }

	public string LastTradePrice { get; set; }

	public string LastExtendedHoursTradePrice { get; set; }

	public string PreviousClose { get; set; }

	public string AdjustedPreviousClose { get; set; }

	public DateTime? PreviousCloseDate { get; set; }

	public string Symbol { get; set; } // "AAPL"

	public bool? TradingHalted { get; set; }

	public bool? HasTraded { get; set; }

	public string LastTradePriceSource { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public string Instrument { get; set; } //Example: "https://api.robinhood.com/instruments/450dfc6d-5510-4d40-abfb-f633b7d9be3e/"

	public Guid? InstrumentId { get; set; } //Example: "450dfc6d-5510-4d40-abfb-f633b7d9be3e"

	public string State { get; set; } //Example: "active"
}
