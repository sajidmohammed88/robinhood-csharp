namespace Rb.Integration.Api.Exceptions;

public class CryptoCurrencyException : Exception
{
	public CryptoCurrencyException()
	{
	}
	public CryptoCurrencyException(string message) : base(message)
	{
	}

	public CryptoCurrencyException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
