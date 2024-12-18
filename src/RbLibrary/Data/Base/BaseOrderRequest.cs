namespace Rb.Integration.Api.Data.Base;

public class BaseOrderRequest
{
	public string Price { get; set; }

	public string Quantity { get; set; }

	public Guid RefId { get; set; } = Guid.NewGuid();

	public Side? Side { get; set; }

	public TimeInForce? TimeInForce { get; set; }

	public OrderType? Type { get; set; }
}
