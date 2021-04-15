using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.User
{
    public class User
    {
        public string Url { get; set; }

        public Guid Id { get; set; }

        [JsonPropertyName("id_info")]
        public string IdInfo { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("origin")]
        public Origin Origin { get; set; }

        [JsonPropertyName("profile_name")]
        public string ProfileName { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
