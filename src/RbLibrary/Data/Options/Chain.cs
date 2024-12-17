namespace Rb.Integration.Api.Data.Options;

public class Chain
{
	public Guid? Id { get; set; }

	public string Symbol { get; set; }

	public bool? CanOpenPosition { get; set; }

	public string CashComponent { get; set; }

	public IList<string> ExpirationDates { get; set; }

	public string TradeValueMultiplier { get; set; }

	public IList<UnderlyingInstrument> UnderlyingInstruments { get; set; }

	public MinTicks MinTicks { get; set; }
}
