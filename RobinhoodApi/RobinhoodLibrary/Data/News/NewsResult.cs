using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.News
{
    public class NewsResult: BaseResult
    {
        public int Count { get; set; }

        public IList<NewsData> Results { get; set; }
    }
}
