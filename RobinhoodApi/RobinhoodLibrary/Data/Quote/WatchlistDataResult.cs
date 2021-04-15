using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Quote
{
    public class WatchlistDataResult : BaseResult
    {
        public IList<WatchlistData> Results { get; set; }
    }
}
