using RobinhoodApi.Data.Authentication;

using System.Net;

namespace RobinhoodApi.Abstractions;

/// <summary>
/// The session manager, that manage the authentication to robinhood API.
/// </summary>
public interface ISessionManager
{
	/// <summary>
	/// Login the user.
	/// </summary>
	/// <returns>The first authentication response.</returns>
	Task<AuthenticationResponse> Login();

	/// <summary>
	/// Challenge the oauth2.
	/// </summary>
	/// <param name="challengeId">The challenge identifier.</param>
	/// <param name="code">The code.</param>
	/// <returns>The authentication response.</returns>
	Task<AuthenticationResponse> ChallengeOauth2(Guid challengeId, string code);

	/// <summary>
	/// Mfa oath2.
	/// </summary>
	/// <param name="code">The code.</param>
	/// <returns>The mfa attempt result.</returns>
	Task<(HttpStatusCode, AuthenticationResponse)> MfaOath2(string code);

	/// <summary>
	/// Configure the manager.
	/// </summary>
	/// <param name="response">The auth response.</param>
	void ConfigureManager(AuthenticationResponse response);

	/// <summary>
	/// Logout.
	/// </summary>
	Task Logout();
}
