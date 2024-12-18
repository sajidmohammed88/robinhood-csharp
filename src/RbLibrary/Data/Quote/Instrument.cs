namespace Rb.Integration.Api.Data.Quote;

public class Instrument
{
	public Guid? Id { get; set; } // Example: "4c9edd7b-f43c-49fa-93aa-9e8f7f4d47a6"

	public string Url { get; set; } // Example: "https://api.robinhood.com/instruments/4c9edd7b-f43c-49fa-93aa-9e8f7f4d47a6/"

	public string Quote { get; set; } // Example: "https://api.robinhood.com/quotes/QBTS/"

	public string Fundamentals { get; set; } // Example: "https://api.robinhood.com/fundamentals/QBTS/"

	public string Splits { get; set; }

	public string State { get; set; } // Example: "Active"

	public string Market { get; set; } // Example: "https://api.robinhood.com/markets/XNYS/"

	public string SimpleName { get; set; } // Example: "D-Wave Quantum"

	public string Name { get; set; } // Example: "D-Wave Quantum Inc."

	public bool? Tradeable { get; set; }

	public string Tradability { get; set; }

	public string Symbol { get; set; } // Example: "QBTS"

	public string BloombergUnique { get; set; }

	public string MarginInitialRatio { get; set; }

	public string MaintenanceRatio { get; set; }

	public string Country { get; set; }

	public string DayTradeRatio { get; set; }

	public DateTime? ListDate { get; set; }

	public string MinTickSize { get; set; }

	public string Type { get; set; } // Example: "stock"

	public string TradableChainId { get; set; }

	public string RhsTradability { get; set; }

	public string FractionalTradability { get; set; }

	public string DefaultCollarFraction { get; set; }

	public string IpoAccessStatus { get; set; }

	public string IpoAccessCobDeadline { get; set; }

	public string IpoAllocatedPrice { get; set; }

	public string IpoCustomersReceived { get; set; }

	public string IpoCustomersRequested { get; set; }

	public string IpoDate { get; set; }

	public string IpoS1Url { get; set; }

	public bool? IsSpac { get; set; }

	public bool? IsTest { get; set; }
}
