using RobinhoodLibrary.Configurations;
using RobinhoodLibrary.Data.Authentication;
using System;
using System.Collections.Generic;

namespace RobinhoodLibrary.Helpers
{
    internal static class AuthHelper
    {
        internal static bool IsOauthValid(string token, string refreshToken) =>
            !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(refreshToken);

        internal static bool IsBearerTokenAboutToExpire(DateTime expirationDate) =>
            expirationDate == DateTime.MinValue || expirationDate <= DateTime.UtcNow;

        internal static IDictionary<string, string> BuildAuthenticationContent(RobinhoodConfiguration configuration, Guid deviceToken) =>
            new Dictionary<string, string>
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
