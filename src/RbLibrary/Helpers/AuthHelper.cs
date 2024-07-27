namespace Rb.Integration.Api.Helpers;

internal static class AuthHelper
{
	internal static bool IsOauthValid(string token, string refreshToken)
	{
		return !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(refreshToken);
	}

	internal static bool IsBearerTokenAboutToExpire(DateTime expirationDate)
	{
		return expirationDate == DateTime.MinValue || expirationDate <= DateTime.UtcNow;
	}

	internal static IDictionary<string, string> BuildAuthenticationContent(AuthConfiguration configuration, Guid deviceToken)
	{
		return new Dictionary<string, string>
		{
			{"password", configuration.Password},
			{"username", configuration.UserName},
			{"grant_type", "password"},
			{"client_id", configuration.ClientId},
			{"expires_in", configuration.ExpirationTime.ToString()},
			{"scope", "internal"},
			{"device_token", deviceToken.ToString()},
			{"challenge_type", configuration.ChallengeType}
		};
	}
}
