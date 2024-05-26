namespace RobinhoodApi.Data.Dividends;

public class Dividends
{
	public Guid Id { get; set; }

	public string Url { get; set; }

	public string Account { get; set; }

	public string Instrument { get; set; }

	public string Amount { get; set; }

	public string Rate { get; set; }

	public string Position { get; set; }

	public string Withholding { get; set; }

	public DateTime RecordDate { get; set; }

	public DateTime PayableDate { get; set; }

	public DateTime PaidAt { get; set; }

	public string State { get; set; }

	public bool DripEnabled { get; set; }

	public string NraWithholding { get; set; }
}
