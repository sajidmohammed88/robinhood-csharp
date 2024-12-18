namespace Rb.Integration.Api.Data.Orders;

public class Execution
{
	public string Price { get; set; }

	public string Quantity { get; set; }

	public string SettlementDate { get; set; }

	public DateTime? Timestamp { get; set; }

	public Guid? Id { get; set; }

	public string IpoAccessExecutionRank { get; set; }
}
