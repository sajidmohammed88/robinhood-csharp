using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Positions
{
    public class PositionResult : BaseResult
    {
        public IList<Position> Results { get; set; }
    }
}
