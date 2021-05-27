using System;

namespace RobinhoodLibrary.Data.Crypto
{
    public class CryptoAccount
    {
        public string Id { get; set; }

        public string ApexAccountNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public string RhsAccountNumber { get; set; }

        public string Status { get; set; }

        public string StatusReasonCode { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UserId { get; set; }
    }
}
