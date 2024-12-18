namespace Rb.Integration.Api.Data.Crypto;

public class CurrencyPair
{
	public CryptoCurrency AssetCurrency { get; set; }

	public bool? DisplayOnly { get; set; }

	public string Id { get; set; }

	public string MaxOrderSize { get; set; }

	public string MinOrderPriceIncrement { get; set; }

	public string MinOrderQuantityIncrement { get; set; }

	public string MinOrderSize { get; set; }

	public string Name { get; set; } //Example: "Bitcoin to US Dollar"

	public CryptoCurrency QuoteCurrency { get; set; }

	public string Symbol { get; set; } //Example: "BTC-USD"

	public string Tradability { get; set; }

	public string Type { get; set; } //Example: "cryptocurrency"
}
