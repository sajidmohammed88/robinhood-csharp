using RobinhoodLibrary.Abstractions;
using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobinhoodLibrary.Services
{
    /// <summary>
    /// The paginator class/
    /// </summary>
    /// <seealso cref="IPaginator" />
    public class Paginator : IPaginator
    {
        private readonly IHttpClientManager _httpClientManager;

        public Paginator(IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager;
        }

        /// <inheritdoc />
        public async Task<IList<T>> PaginateResultAsync<T>(BaseResult<T> baseResult)
        {
            if (baseResult?.Results == null || !baseResult.Results.Any() || baseResult.Next == null)
            {
                return baseResult?.Results;
            }

            List<T> result = new List<T>();
            result.AddRange(baseResult.Results);

            while (baseResult.Next != null)
            {
                baseResult = await _httpClientManager.GetAsync<BaseResult<T>>(baseResult.Next);
                result.AddRange(baseResult.Results);
            }

            return result;
        }
    }
}
