namespace Rb.Integration.Api.Data.Options;

public class Option
{
	public string Account { get; set; }

	public string AveragePrice { get; set; }

	public string ChainId { get; set; }

	public string ChainSymbol { get; set; }

	public Guid Id { get; set; }

	[JsonPropertyName("option")]
	public string OptionUrl { get; set; }

	public string Type { get; set; }

	public string PendingBuyQuantity { get; set; }

	public string PendingExpiredQuantity { get; set; }

	public string PendingExpirationQuantity { get; set; }

	public string PendingExerciseQuantity { get; set; }

	public string PendingAssignmentQuantity { get; set; }

	public string PendingSellQuantity { get; set; }

	public string Quantity { get; set; }

	public string IntradayQuantity { get; set; }

	public string IntradayAverageOpenPrice { get; set; }

	public DateTime CreatedAt { get; set; }

	public string TradeValueMultiplier { get; set; }

	public DateTime UpdatedAt { get; set; }

	public string Url { get; set; }

	public string OptionId { get; set; }
}
