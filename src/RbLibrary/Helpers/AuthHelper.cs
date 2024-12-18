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

	internal static IDictionary<string, object> BuildAuthenticationContent(AuthConfiguration configuration)
	{
		return new Dictionary<string, object>
		{
			{"password", configuration.Password},
			{"username", configuration.UserName},
			{"grant_type", Constants.Authentication.GrantType},
			{"client_id", Constants.Authentication.ClientId},
			{"expires_in", configuration.ExpirationTime.ToString()},
			{"scope", Constants.Authentication.Scope},
			{"challenge_type", configuration.ChallengeType},
			{"device_token", configuration.DeviceToken}
		};
	}
}
