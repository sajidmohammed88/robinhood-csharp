﻿namespace Rb.Integration.Api.Exceptions;

public class AuthenticationException : Exception
{
	public AuthenticationException()
	{
	}

	public AuthenticationException(string message) : base(message)
	{
	}

	public AuthenticationException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
