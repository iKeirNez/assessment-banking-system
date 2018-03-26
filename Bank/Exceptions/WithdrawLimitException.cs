using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    /// <summary>
    /// An exception which is thrown when the operation would cause the account to exceed it's withdrawal limit.
    /// </summary>
    public class WithdrawLimitException : Exception
    {
        public WithdrawLimitException()
        {
        }

        public WithdrawLimitException(string message) : base(message)
        {
        }

        public WithdrawLimitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WithdrawLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
