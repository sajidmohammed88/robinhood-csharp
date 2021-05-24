using System.Collections.Generic;

namespace RobinhoodLibrary.Data.Base
{
    public class BaseResult<T>
    {
        public string Next { get; set; }

        public string Previous { get; set; }

        // Used just for news
        public int Count { get; set; }

        public IList<T> Results { get; set; }
    }
}
