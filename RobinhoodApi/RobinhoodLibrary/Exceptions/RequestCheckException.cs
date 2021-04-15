using System;

namespace RobinhoodLibrary.Exceptions
{
    public class RequestCheckException : Exception
    {
        public RequestCheckException()
        {
        }
        public RequestCheckException(string message) : base(message)
        {
        }
    }
}
