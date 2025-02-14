﻿namespace Rb.Integration.Api.Data.Crypto;

public class CryptoAccount
{
	public string AccountNumber { get; set; }

	public string AccountType { get; set; }

	public string Id { get; set; }

	public string ApexAccountNumber { get; set; }

	public DateTime? CreatedAt { get; set; }

	public string RhsAccountNumber { get; set; }

	public string Status { get; set; }

	public string StatusReasonCode { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public string UserId { get; set; }
}
