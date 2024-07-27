namespace Rb.Integration.Api.Services;

/// <summary>
/// The paginator class
/// </summary>
/// <seealso cref="IPaginator" />
public class Paginator(IHttpClientManager httpClientManager) : IPaginator
{

	/// <inheritdoc />
	public async Task<IList<T>> PaginateResultAsync<T>(BaseResult<T> baseResult)
	{
		if (baseResult?.Results is null || !baseResult.Results.Any() || baseResult.Next == null)
		{
			return baseResult?.Results;
		}

		List<T> result = [.. baseResult.Results];

		while (baseResult.Next != null)
		{
			baseResult = await httpClientManager.GetAsync<BaseResult<T>>(baseResult.Next);
			result.AddRange(baseResult.Results);
		}

		return result;
	}
}
