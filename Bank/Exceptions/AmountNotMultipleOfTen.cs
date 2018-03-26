using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    public class AmountNotMultipleOfTen : Exception
    {
        public AmountNotMultipleOfTen()
        {
        }

        public AmountNotMultipleOfTen(string message) : base(message)
        {
        }

        public AmountNotMultipleOfTen(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AmountNotMultipleOfTen(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
