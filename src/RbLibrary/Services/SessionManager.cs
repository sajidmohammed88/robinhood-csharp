using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

using Rb.Integration.Api.Abstractions;
using Rb.Integration.Api.Configurations;
using Rb.Integration.Api.Data.Authentication;
using Rb.Integration.Api.Exceptions;
using Rb.Integration.Api.Helpers;

using System.Net;
using System.Text;
using System.Text.Json;

namespace Rb.Integration.Api.Services;

/// <summary>
/// The session manager, that manage the authentication to robinhood API.
/// </summary>
/// <seealso cref="ISessionManager" />
public class SessionManager : ISessionManager, IHttpClientManager
{
	private readonly AuthConfiguration _configuration;
	private readonly JsonSerializerOptions _settings;
	private readonly HttpClient _httpClient;
	private DateTime _expirationDate;
	private int _expireIn;
	private string _token;
	private string _refreshToken;
	private readonly Guid _deviceToken;
	private bool _disposedValue;

	public SessionManager(IOptions<AuthConfiguration> options, IHttpClientFactory httpClientFactory)
	{
		ArgumentNullException.ThrowIfNull(httpClientFactory);
		_configuration = options?.Value ?? throw new ArgumentNullException(nameof(options));
		_settings = CustomJsonSerializerOptions.Current;
		_deviceToken = Guid.NewGuid();
		_httpClient = PrepareHttpClientHeader(httpClientFactory);
	}

	/// <inheritdoc />
	public async Task<T> GetAsync<T>(string url, bool autoLog = true, IDictionary<string, string> query = null)
	{
		if (query != null && query.Any())
		{
			url = QueryHelpers.AddQueryString(url, query);
		}

		HttpResponseMessage response = await _httpClient.GetAsync(url);

		if (response.StatusCode == HttpStatusCode.Unauthorized && autoLog)
		{
			ConfigureManager(await RefreshOauth2Async());

			response = await _httpClient.GetAsync(url);
		}

		if (!response.IsSuccessStatusCode)
		{
			throw new HttpResponseException($"The get call is faulted for {url} with status code : {response.StatusCode}");
		}

		return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), _settings);
	}

	/// <inheritdoc />
	public async Task<T> PostJsonAsync<T>(string url, string jsonRequest, bool autoLog = true) where T : class
	{
		HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

		if (response.StatusCode == HttpStatusCode.Unauthorized && autoLog)
		{
			ConfigureManager(await RefreshOauth2Async());

			response = await _httpClient.PostAsync(url, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
		}

		string result = await response.Content.ReadAsStringAsync();

		if (response.StatusCode != HttpStatusCode.Created)
		{
			throw new HttpResponseException($"The post call is faulted for {url} with status code : {response.StatusCode} and message : {result}");
		}

		return JsonSerializer.Deserialize<T>(result, _settings);
	}

	/// <inheritdoc />
	public async Task<(HttpStatusCode StatusCode, T Data)> PostAsync<T>(string url,
			IDictionary<string, string> data, bool autoLog = true) where T : class
	{
		string body = data != null && data.Any()
				? string.Join("&", data.Where(v => !string.IsNullOrEmpty(v.Value)).Select(_ => $"{_.Key}={_.Value}"))
				: "";

		HttpResponseMessage response = await _httpClient.PostAsync(url,
				new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"));

		if (response.StatusCode == HttpStatusCode.Unauthorized && autoLog)
		{
			ConfigureManager(await RefreshOauth2Async());

			response = await _httpClient.PostAsync(url,
					new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"));
		}

		string responseValue = await response.Content.ReadAsStringAsync();

		return (response.StatusCode, !string.IsNullOrEmpty(responseValue) ? JsonSerializer.Deserialize<T>(responseValue, _settings) : null);
	}

	/// <inheritdoc />
	public async Task<(HttpStatusCode StatusCode, string Result)> PostAsync(string url,
					IDictionary<string, string> data, (string Name, string Value) specifiedHeader)
	{
		if (specifiedHeader != (null, null) && !_httpClient.DefaultRequestHeaders.Contains(specifiedHeader.Name))
		{
			_httpClient.DefaultRequestHeaders.Add(specifiedHeader.Name, specifiedHeader.Value);
		}

		StringContent content = null;
		if (data != null && data.Any())
		{
			string body = string.Join("&",
					data.Where(v => !string.IsNullOrEmpty(v.Value)).Select(_ => $"{_.Key}={_.Value}"));
			content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
		}

		HttpResponseMessage response = await _httpClient.PostAsync(url, content);

		return (response.StatusCode, await response.Content.ReadAsStringAsync());
	}

	/// <inheritdoc />
	public async Task<AuthenticationResponse> Login()
	{
		if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
		{
			return await LoginOauth2Async();
		}

		if (AuthHelper.IsOauthValid(_token, _refreshToken) && AuthHelper.IsBearerTokenAboutToExpire(_expirationDate))
		{
			return await RefreshOauth2Async();
		}

		return new AuthenticationResponse
		{
			AccessToken = _token,
			RefreshToken = _refreshToken,
			ExpiresIn = _expireIn
		};
	}

	/// <inheritdoc />
	public async Task<AuthenticationResponse> ChallengeOauth2(Guid challengeId, string code)
	{
		var (statusCode, result) = await PostAsync(string.Format(Constants.Routes.Challenge, challengeId),
				new Dictionary<string, string> { { "response", code } },
				("X-ROBINHOOD-CHALLENGE-RESPONSE-ID", challengeId.ToString()));

		try
		{
			if (statusCode != HttpStatusCode.OK)
			{
				return JsonSerializer.Deserialize<AuthenticationResponse>(result, _settings);
			}

			var response = await PostAsync<AuthenticationResponse>(Constants.Routes.Oauth,
					AuthHelper.BuildAuthenticationContent(_configuration, _deviceToken));

			return response.Data;
		}
		catch (Exception ex)
		{
			throw new AuthenticationException($"Error in finalizing auth token, error : {ex.Message}");
		}
	}

	/// <inheritdoc />
	public async Task<(HttpStatusCode, AuthenticationResponse)> MfaOath2(string code)
	{
		IDictionary<string, string> authContent = AuthHelper.BuildAuthenticationContent(_configuration, _deviceToken);
		authContent.Add("mfa_code", code);

		return await PostAsync<AuthenticationResponse>(Constants.Routes.Oauth, authContent);
	}

	/// <inheritdoc />
	public void ConfigureManager(AuthenticationResponse response)
	{
		_expirationDate = DateTime.UtcNow.AddSeconds(response.ExpiresIn);
		_token = response.AccessToken;
		_refreshToken = response.RefreshToken;
		_expireIn = response.ExpiresIn;

		if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
		{
			_httpClient.DefaultRequestHeaders.Remove("Authorization");
		}
		_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
	}

	/// <inheritdoc />
	public async Task Logout()
	{
		IDictionary<string, string> logoutContent = new Dictionary<string, string>
					{
							{"client_id", _configuration.ClientId},
							{"token", _token}
					};

		try
		{
			await PostAsync<AuthenticationResponse>(Constants.Routes.OauthRevoke, logoutContent);

			_token = null;
			_refreshToken = null;
			_expirationDate = DateTime.MinValue;
			_httpClient.DefaultRequestHeaders.Clear();
		}
		catch (Exception ex)
		{
			throw new AuthenticationException($"Could not log out : {ex.Message}");
		}
	}

	private async Task<AuthenticationResponse> LoginOauth2Async()
	{
		var authentication = await PostAsync<AuthenticationResponse>(Constants.Routes.Oauth,
				AuthHelper.BuildAuthenticationContent(_configuration, _deviceToken));

		return authentication.Data;
	}

	private async Task<AuthenticationResponse> RefreshOauth2Async()
	{
		if (!AuthHelper.IsOauthValid(_token, _refreshToken))
		{
			throw new AuthenticationException("Cannot refresh login with unset refresh token");
		}

		_httpClient.DefaultRequestHeaders.Remove("Authorization");

		IDictionary<string, string> refreshContent = new Dictionary<string, string>
					{
							{"grant_type", "refresh_token"},
							{"refresh_token", _refreshToken},
							{"scope", "internal"},
							{"client_id", _configuration.ClientId},
							{"expires_in", _configuration.ExpirationTime.ToString()}
					};

		var authentication = await PostAsync<AuthenticationResponse>(Constants.Routes.Oauth, refreshContent);

		return authentication.Data;
	}

	private HttpClient PrepareHttpClientHeader(IHttpClientFactory httpClientFactory)
	{
		HttpClient client = httpClientFactory.CreateClient();
		client.Timeout = TimeSpan.FromSeconds(_configuration.Timeout);

		client.DefaultRequestHeaders.Clear();
		client.DefaultRequestHeaders.ConnectionClose = false;
		client.DefaultRequestHeaders.Add("Accept", "*/*");
		client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
		client.DefaultRequestHeaders.Add("Accept-Language", "en;q=1, fr;q=0.9, de;q=0.8, ja;q=0.7, nl;q=0.6, it;q=0.5");
		client.DefaultRequestHeaders.Add("X-Robinhood-API-Version", "1.0.0");
		client.DefaultRequestHeaders.Add("Origin", "https://robinhood.com");
		client.DefaultRequestHeaders.Add("User-Agent", "Robinhood/823 (iPhone; iOS 7.1.2; Scale/2.00) Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36");

		return client;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (_disposedValue)
		{
			return;
		}

		if (disposing)
		{
			_httpClient.Dispose();
		}

		_disposedValue = true;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
