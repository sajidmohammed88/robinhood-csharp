using RobinhoodApi.Data.Base;

namespace RobinhoodApi.Data.Crypto.Request;

public class CryptoOrderRequest : BaseOrderRequest
{
	public string AccountId { get; set; }

	public string CurrencyPairId { get; set; }

	public Guid RefId { get; set; } = Guid.NewGuid();
}
