namespace Rb.Integration.Api.Exceptions;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
public class RequestCheckException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
{
	public RequestCheckException(string message) : base(message)
	{
	}

	public RequestCheckException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
