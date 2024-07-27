namespace Rb.Integration.Api.Abstractions;

/// <summary>
/// The session manager, that manage the authentication to robinhood API.
/// </summary>
public interface ISessionManager
{
	/// <summary>
	/// Login the user.
	/// </summary>
	/// <returns>The first authentication response.</returns>
	Task<AuthenticationResponse> LoginAsync();

	/// <summary>
	/// Challenge the oauth2.
	/// </summary>
	/// <param name="challengeId">The challenge identifier.</param>
	/// <param name="code">The code.</param>
	/// <returns>The authentication response.</returns>
	Task<AuthenticationResponse> ChallengeOauth2Async(Guid challengeId, string code);

	/// <summary>
	/// Mfa oath2.
	/// </summary>
	/// <param name="code">The code.</param>
	/// <returns>The mfa attempt result.</returns>
	Task<(HttpStatusCode, AuthenticationResponse)> MfaOath2Async(string code);

	/// <summary>
	/// Configure the manager.
	/// </summary>
	/// <param name="response">The auth response.</param>
	void ConfigureManager(AuthenticationResponse response);

	/// <summary>
	/// Logout.
	/// </summary>
	Task LogoutAsync();
}
