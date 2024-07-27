namespace Rb.Integration.Api.Exceptions;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
public class AuthenticationException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
{
	public AuthenticationException(string message) : base(message)
	{
	}

	public AuthenticationException(string message, Exception innerException) : base(message, innerException)
	{
	}

	protected AuthenticationException(SerializationInfo info, StreamingContext context)
	{
	}
}
