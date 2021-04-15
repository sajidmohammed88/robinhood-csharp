using System;

namespace RobinhoodLibrary.Exceptions
{
    internal class HttpResponseException : Exception
    {
        public HttpResponseException()
        {
        }

        public HttpResponseException(string message) : base(message)
        {
        }
    }
}
