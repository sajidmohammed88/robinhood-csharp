namespace Rb.Integration.Api.Data.Crypto;

public class CurrencyPair
{
	public CryptoCurrency AssetCurrency { get; set; }

	public bool DisplayOnly { get; set; }

	public string Id { get; set; }

	public string MaxOrderSize { get; set; }

	public string MinOrderPriceIncrement { get; set; }

	public string MinOrderQuantityIncrement { get; set; }

	public string MinOrderSize { get; set; }

	public string Name { get; set; }

	public CryptoCurrency QuoteCurrency { get; set; }

	public string Symbol { get; set; }

	public string Tradability { get; set; }
}
