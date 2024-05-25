namespace RobinhoodApi.Data.Quote;

public class Historical
{
	public DateTime BeginsAt { get; set; }

	public string OpenPrice { get; set; }

	public string ClosePrice { get; set; }

	public string HighPrice { get; set; }

	public string LowPrice { get; set; }

	public int Volume { get; set; }

	public string Session { get; set; }

	public bool Interpolated { get; set; }
}
