namespace Rb.Integration.Api.Data.Crypto;

public class Holding
{
	public string AccountId { get; set; }

	public List<CostBase> CostBases { get; set; }

	public DateTime? CreatedAt { get; set; }

	public Currency Currency { get; set; }

	public string Id { get; set; }

	public string Quantity { get; set; }

	public string QuantityAvailable { get; set; }

	public string QuantityHeld { get; set; }

	public string QuantityHeldForBuy { get; set; }

	public string QuantityHeldForSell { get; set; }

	public DateTime? UpdatedAt { get; set; }
}
