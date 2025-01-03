﻿namespace Rb.Integration.Api.Data.Base;

public class BaseOrder : BaseDetail
{
	public Guid? Id { get; set; }

	public Guid? RefId { get; set; }

	public DateTime? CreatedAt { get; set; }

	public string CumulativeQuantity { get; set; }

	public DateTime? LastTransactionAt { get; set; }

	public string Price { get; set; }

	public string Quantity { get; set; }

	public Side? Side { get; set; } //Example: "buy"

	public string State { get; set; } //Example: "filled"

	public TimeInForce? TimeInForce { get; set; } //Example: "gtc"

	public OrderType? Type { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public List<Execution> Executions { get; set; }

	public string AveragePrice { get; set; }
}
