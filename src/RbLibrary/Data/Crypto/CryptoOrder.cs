using RobinhoodApi.Data.Base;

namespace RobinhoodApi.Data.Crypto;

public class CryptoOrder : BaseOrder
{
	public string AccountId { get; set; }

	public string CancelUrl { get; set; }

	public string RoundedExecutedNotional { get; set; }

	public Guid CurrencyPairId { get; set; }
}
