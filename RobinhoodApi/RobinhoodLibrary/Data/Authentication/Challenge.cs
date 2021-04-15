using RobinhoodLibrary.Enum;
using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.Authentication
{
    public class Challenge
    {
        public Guid Id { get; set; }

        public Guid User { get; set; }

        [JsonPropertyName("type")]
        public ChallengeType ChallengeType { get; set; }

        [JsonPropertyName("alternate_type")]
        public ChallengeType? AlternateType { get; set; }

        public Status Status { get; set; }

        [JsonPropertyName("remaining_retries")]
        public int RemainingRetries { get; set; }

        [JsonPropertyName("remaining_attempts")]
        public int RemainingAttempts { get; set; }

        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; }

        public bool CanRetry => RemainingAttempts > 0 && DateTime.UtcNow < ExpiresAt;
    }
}