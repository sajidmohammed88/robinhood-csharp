using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Quote
{
    public class InstrumentsTag
    {
        public string CanonicalExamples { get; set; }

        public string Description { get; set; }

        public IList<string> Instruments { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int MembershipCount { get; set; }
    }
}
