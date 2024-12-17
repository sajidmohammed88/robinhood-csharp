namespace Rb.Integration.Api.Data.User;

public class CashBalances
{
	public string UnclearedDeposits { get; set; }

	public string Cash { get; set; }

	public string CryptoBuyingPower { get; set; }

	public string PortfolioCash { get; set; }

	public string BuyingPower { get; set; }

	public DateTime? CreatedAt { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public bool? IsPrimaryAccount { get; set; }
}
