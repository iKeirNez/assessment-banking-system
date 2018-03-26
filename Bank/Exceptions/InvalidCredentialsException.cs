using System;
using System.Runtime.Serialization;

namespace Bank.Exceptions
{
    /// <summary>
    /// An exception which is thrown when the user attempts to authenticate with invalid credentials.
    /// </summary>
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
        {
        }

        public InvalidCredentialsException(string message) : base(message)
        {
        }

        public InvalidCredentialsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
