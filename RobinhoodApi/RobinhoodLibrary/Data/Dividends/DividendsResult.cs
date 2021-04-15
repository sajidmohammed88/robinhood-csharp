using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Dividends
{
    public class DividendsResult : BaseResult
    {
        public IList<Dividends> Results { get; set; }
    }
}
