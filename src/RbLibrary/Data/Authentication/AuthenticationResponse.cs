namespace Rb.Integration.Api.Data.Authentication;

public class AuthenticationResponse : BaseDetail
{
	public Challenge Challenge { get; set; }

	public bool MfaRequired { get; set; }

	public string AccessToken { get; set; }

	public string RefreshToken { get; set; }

	public int ExpiresIn { get; set; }

	public string Error { get; set; }

	[JsonIgnore]
	public bool IsChallenge => Challenge != null;

	[JsonIgnore]
	public bool IsOauthValid => !string.IsNullOrEmpty(AccessToken) && !string.IsNullOrEmpty(RefreshToken);

	public VerificationWorkflow VerificationWorkflow { get; set; }
}

