using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.News
{
    public class NewsData
    {
        [JsonPropertyName("api_source")]
        public string ApiSource { get; set; }

        public string Author { get; set; }

        [JsonPropertyName("num_clicks")]
        public int NumClicks { get; set; }

        [JsonPropertyName("preview_image_url")]
        public string PreviewImageUrl { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("relay_url")]
        public string RelayUrl { get; set; }

        public string Source { get; set; }

        public string Summary { get; set; }

        public string Title { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public string Url { get; set; }

        public Guid Uuid { get; set; }

        [JsonPropertyName("related_instruments")]
        public IList<string> RelatedInstruments { get; set; }

        [JsonPropertyName("preview_text")]
        public string PreviewText { get; set; }

        [JsonPropertyName("currency_id")]
        public string CurrencyId { get; set; }
    }
}
