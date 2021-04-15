using System;
using System.Text.Json.Serialization;

namespace RobinhoodLibrary.Data.User
{
    public class InstantEligibility
    {
        public string Reason { get; set; }

        [JsonPropertyName("reinstatement_date")]
        public DateTime? ReinstatementDate { get; set; }

        public string Reversal { get; set; }

        public string State { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("additional_deposit_needed")]
        public string AdditionalDepositNeeded { get; set; }

        [JsonPropertyName("compliance_user_major_oak_email")]
        public string ComplianceUserMajorOakEmail { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
    }
}
