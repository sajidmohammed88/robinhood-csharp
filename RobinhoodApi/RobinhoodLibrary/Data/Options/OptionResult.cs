using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Options
{
    public class OptionResult : BaseResult
    {
        public IList<Option> Results { get; set; }
    }
}
