namespace Rb.Integration.Api.Exceptions;

public class RequestCheckException : Exception
{
	public RequestCheckException()
	{
	}
	public RequestCheckException(string message) : base(message)
	{
	}

	public RequestCheckException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
