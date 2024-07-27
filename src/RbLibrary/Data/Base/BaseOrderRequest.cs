namespace Rb.Integration.Api.Data.Base;

public class BaseOrderRequest
{
	public string Price { get; set; }

	public Side Side { get; set; }

	public TimeInForce TimeInForce { get; set; }

	public OrderType Type { get; set; }

	public int Quantity { get; set; }
}
