using Microsoft.Extensions.Options;

namespace Rb.Integration.Api.Configurations;

/// <summary>
/// The robinhood configuration.
/// </summary>
/// <seealso cref="IOptions{RobinhoodAuthentication}" />
public class AuthConfiguration : IOptions<AuthConfiguration>
{
	public const string Authentication = "Authentication";

	public string UserName { get; set; }

	public string Password { get; set; }

	public string ClientId { get; set; }

	public int ExpirationTime { get; set; }

	public int Timeout { get; set; }

	public string ChallengeType { get; set; }

	AuthConfiguration IOptions<AuthConfiguration>.Value => this;
}
