using RobinhoodApi.Data.Base;
using RobinhoodApi.Data.Quote;

namespace RobinhoodApi.Data.Crypto;

public class CryptoHistoricalData : BaseHistoricalData
{
	public IList<Historical> DataPoints { get; set; }

	public Guid Id { get; set; }
}
