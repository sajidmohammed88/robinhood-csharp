namespace Rb.Integration.Api.Data.Crypto;

public class CryptoOrder : BaseOrder
{
	public string AccountId { get; set; }

	public string CancelUrl { get; set; }

	public string CurrencyCode { get; set; } // Example: "LTC"

	public Guid? CurrencyPairId { get; set; }

	public string RoundedExecutedNotional { get; set; }
}
