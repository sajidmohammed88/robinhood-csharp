using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Options
{
    public class MinTicks
    {
        [JsonPropertyName("above_tick")]
        public string AboveTick { get; set; }

        [JsonPropertyName("below_tick")]
        public string BelowTick { get; set; }

        [JsonPropertyName("cutoff_price")]
        public string CutoffPrice { get; set; }
    }
}
