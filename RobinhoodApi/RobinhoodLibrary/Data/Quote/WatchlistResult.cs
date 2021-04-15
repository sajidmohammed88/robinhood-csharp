using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Quote
{
    public class WatchlistResult : BaseResult
    {
        public IList<Watchlist> Results { get; set; }
    }
}
