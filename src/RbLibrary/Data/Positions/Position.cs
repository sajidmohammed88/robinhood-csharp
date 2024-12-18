namespace Rb.Integration.Api.Data.Positions;

public class Position
{
	public string Url { get; set; } // Example: "https://api.robinhood.com/positions/5SJ45118/450dfc6d-5510-4d40-abfb-f633b7d9be3e/"

	public string Instrument { get; set; } // Example: "https://api.robinhood.com/instruments/450dfc6d-5510-4d40-abfb-f633b7d9be3e/"

	public string Symbol { get; set; } // Example: "AAPL"

	public string Account { get; set; }

	public string AccountNumber { get; set; }

	public string AverageBuyPrice { get; set; }

	public string PendingAverageBuyPrice { get; set; }

	public string Quantity { get; set; }

	public string IntradayAverageBuyPrice { get; set; }

	public string IntradayQuantity { get; set; }

	public string SharesAvailableForExercise { get; set; }

	public string SharesHeldForBuys { get; set; }

	public string SharesHeldForSells { get; set; }

	public string SharesHeldForStockGrants { get; set; }

	public string SharesHeldForOptionsCollateral { get; set; }

	public string SharesHeldForOptionsEvents { get; set; }

	public string SharesPendingFromOptionsEvents { get; set; }

	public string SharesAvailableForClosingShortPosition { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public DateTime? CreatedAt { get; set; }
}
