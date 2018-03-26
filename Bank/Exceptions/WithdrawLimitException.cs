using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
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
