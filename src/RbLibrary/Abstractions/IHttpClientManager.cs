namespace Rb.Integration.Api.Abstractions;

/// <summary>
/// HTTP client manager interface.
/// </summary>
public interface IHttpClientManager : IDisposable
{
	/// <summary>
	/// Get data asynchronously.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="url">The URL.</param>
	/// <param name="autoLog">The auto log.</param>
	/// <param name="query">Dictionary that represent the query string.</param>
	/// <returns>The result of get request.</returns>
	Task<T> GetAsync<T>(string url, bool autoLog = true, IDictionary<string, string> query = null);

	/// <summary>
	/// Posts the json by serialization of object asynchronous.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="url">The URL.</param>
	/// <param name="request">The request object.</param>
	/// <param name="autoLog">if set to <c>true</c> [automatic log].</param>
	/// <returns>Post result.</returns>
	Task<(HttpStatusCode StatusCode, T Data)> PostAsync<T>(string url, object request, bool autoLog = true) where T : class;

	/// <summary>
	/// Posts the asynchronous.
	/// </summary>
	/// <param name="url">The URL.</param>
	/// <param name="data">The data.</param>
	/// <param name="specifiedHeader">The specified header.</param>
	/// <returns>The status code</returns>
	Task<(HttpStatusCode StatusCode, string Result)> PostAsync(string url, IDictionary<string, object> data, (string Name, string Value) specifiedHeader);
}
