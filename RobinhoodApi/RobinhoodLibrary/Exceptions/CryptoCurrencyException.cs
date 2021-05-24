using System;

namespace RobinhoodLibrary.Exceptions
{
    public class CryptoCurrencyException : Exception
    {
        public CryptoCurrencyException()
        {
        }
        public CryptoCurrencyException(string message) : base(message)
        {
        }
    }
}
