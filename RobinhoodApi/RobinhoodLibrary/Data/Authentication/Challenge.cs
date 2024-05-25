using RobinhoodApi.Enum;

using System.Text.Json.Serialization;

namespace RobinhoodApi.Data.Authentication;

public class Challenge
{
	public Guid Id { get; set; }

	public Guid User { get; set; }

	public ChallengeType Type { get; set; }

	public ChallengeType? AlternateType { get; set; }

	public Status Status { get; set; }

	public int RemainingRetries { get; set; }

	public int RemainingAttempts { get; set; }

	public DateTime ExpiresAt { get; set; }

	[JsonIgnore]
	public bool CanRetry => RemainingAttempts > 0 && DateTime.UtcNow < ExpiresAt;
}