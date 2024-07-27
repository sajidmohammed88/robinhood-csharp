namespace Rb.Integration.Api.Exceptions;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
public class HttpResponseException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
{
	public HttpResponseException(string message) : base(message)
	{
	}

	public HttpResponseException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
