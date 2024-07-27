namespace Rb.Integration.Api.Abstractions;

/// <summary>
/// The paginator interface.
/// </summary>
public interface IPaginator
{
	/// <summary>
	/// Paginates the result asynchronous.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="baseResult">The base result.</param>
	/// <returns>Collection of T</returns>
	Task<IList<T>> PaginateResultAsync<T>(BaseResult<T> baseResult);
}
