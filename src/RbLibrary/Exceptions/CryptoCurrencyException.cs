namespace Rb.Integration.Api.Exceptions;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
public class CryptoCurrencyException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
{
	public CryptoCurrencyException(string message) : base(message)
	{
	}

	public CryptoCurrencyException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
