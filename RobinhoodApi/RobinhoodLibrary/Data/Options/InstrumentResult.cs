using RobinhoodLibrary.Data.Base;
using RobinhoodLibrary.Data.Quote;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Options
{
    public class InstrumentResult : BaseResult
    {
        public IList<Instrument> Results { get; set; }
    }
}
