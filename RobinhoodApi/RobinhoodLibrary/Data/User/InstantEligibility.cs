using System;

namespace RobinhoodLibrary.Data.User
{
    public class InstantEligibility
    {
        public string Reason { get; set; }

        public DateTime? ReinstatementDate { get; set; }

        public string Reversal { get; set; }

        public string State { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string AdditionalDepositNeeded { get; set; }

        public string ComplianceUserMajorOakEmail { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}
