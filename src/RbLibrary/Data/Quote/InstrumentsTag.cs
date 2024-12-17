namespace Rb.Integration.Api.Data.Quote;

public class InstrumentsTag
{
	public string CanonicalExamples { get; set; }

	public string Description { get; set; }

	public IList<string> Instruments { get; set; }

	public string Name { get; set; } // Example: "Top Movers"

	public string Slug { get; set; } // Example: "top-movers"

	public int? MembershipCount { get; set; }
}
