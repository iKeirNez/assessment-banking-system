using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    /// <summary>
    /// An exception which is thrown when there is an issue with a user entered amount (e.g. negative).
    /// </summary>
    public class AmountException : Exception
    {
        public AmountException()
        {
        }

        public AmountException(string message) : base(message)
        {
        }

        public AmountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AmountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
