namespace Rb.Integration.Api.Data.Crypto;

public class CryptoHistoricalData : BaseHistoricalData
{
	public IList<Historical> DataPoints { get; set; }

	public Guid Id { get; set; }
}
