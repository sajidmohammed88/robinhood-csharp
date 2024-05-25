namespace RobinhoodApi.Data.News;

public class NewsData
{
	public string ApiSource { get; set; }

	public string Author { get; set; }

	public int NumClicks { get; set; }

	public string PreviewImageUrl { get; set; }

	public DateTime PublishedAt { get; set; }

	public string RelayUrl { get; set; }

	public string Source { get; set; }

	public string Summary { get; set; }

	public string Title { get; set; }

	public DateTime UpdatedAt { get; set; }

	public string Url { get; set; }

	public Guid Uuid { get; set; }

	public IList<string> RelatedInstruments { get; set; }

	public string PreviewText { get; set; }

	public string CurrencyId { get; set; }
}
