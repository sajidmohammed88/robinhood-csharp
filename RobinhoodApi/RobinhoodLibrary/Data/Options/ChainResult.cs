using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Options
{
    public class ChainResult : BaseResult
    {
        public IList<Chain> Results { get; set; }
    }
}
