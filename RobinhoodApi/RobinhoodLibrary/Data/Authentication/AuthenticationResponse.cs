using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Authentication
{
    public class AuthenticationResponse
    {
        public string Detail { get; set; }

        public Challenge Challenge { get; set; }

        [JsonPropertyName("mfa_required")]
        public bool MfaRequired { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        public string Error { get; set; }

        public bool IsChallenge => Challenge != null;

        public bool IsOauthValid => !string.IsNullOrEmpty(AccessToken) && !string.IsNullOrEmpty(RefreshToken);
    }
}
