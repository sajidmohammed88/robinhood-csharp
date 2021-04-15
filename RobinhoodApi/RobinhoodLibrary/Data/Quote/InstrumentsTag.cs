using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Quote
{
    public class InstrumentsTag
    {
        [JsonPropertyName("canonical_examples")]
        public string CanonicalExamples { get; set; }

        public string Description { get; set; }

        public IList<string> Instruments { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        [JsonPropertyName("membership_count")]
        public int MembershipCount { get; set; }
    }
}
