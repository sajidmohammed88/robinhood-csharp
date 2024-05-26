using Rb.Integration.Api.Data.Base;
using Rb.Integration.Api.Data.Quote;

namespace Rb.Integration.Api.Data.Crypto;

public class CryptoHistoricalData : BaseHistoricalData
{
	public IList<Historical> DataPoints { get; set; }

	public Guid Id { get; set; }
}
