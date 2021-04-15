using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Fundamentals
{
    public class Fundamental
    {
        public string Open { get; set; }

        public string High { get; set; }

        public string Low { get; set; }

        public string Volume { get; set; }

        [JsonPropertyName("market_date")]
        public string MarketDate { get; set; }

        [JsonPropertyName("average_volume_2_weeks")]
        public string AverageVolume2Weeks { get; set; }

        [JsonPropertyName("average_volume")]
        public string AverageVolume { get; set; }

        [JsonPropertyName("high_52_weeks")]
        public string High52Weeks { get; set; }

        [JsonPropertyName("dividend_yield")]
        public string DividendYield { get; set; }

        public string Float { get; set; }

        [JsonPropertyName("low_52_weeks")]
        public string Low52Weeks { get; set; }

        [JsonPropertyName("market_cap")]
        public string MarketCap { get; set; }

        [JsonPropertyName("pb_ratio")]
        public string PbRatio { get; set; }

        [JsonPropertyName("pe_ratio")]
        public string PeRatio { get; set; }

        [JsonPropertyName("shares_outstanding")]
        public string SharesOutstanding { get; set; }

        public string Description { get; set; }

        public string Instrument { get; set; }

        public string Ceo { get; set; }

        [JsonPropertyName("headquarters_city")]
        public string HeadquartersCity { get; set; }

        [JsonPropertyName("headquarters_state")]
        public string HeadquartersState { get; set; }

        public string Sector { get; set; }

        public string Industry { get; set; }

        [JsonPropertyName("num_employees")]
        public int NumEmployees { get; set; }

        [JsonPropertyName("year_founded")]
        public int YearFounded { get; set; }
    }
}
