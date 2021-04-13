using Microsoft.Extensions.Options;

namespace RobinhoodLibrary.Configurations
{
    /// <summary>
    /// The robinhood configuration.
    /// </summary>
    /// <seealso cref="IOptions{RobinhoodAuthentication}" />
    public class RobinhoodConfiguration : IOptions<RobinhoodConfiguration>
    {
        public const string Authentication = "Authentication";

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ClientId { get; set; }

        public int ExpirationTime { get; set; }

        public int Timeout { get; set; }

        public string ChallengeType { get; set; }

        RobinhoodConfiguration IOptions<RobinhoodConfiguration>.Value => this;
    }
}
