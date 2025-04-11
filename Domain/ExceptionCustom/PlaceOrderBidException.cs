using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ExceptionCustom
{
    public class PlaceOrderBidException : Exception
    {
        public PlaceOrderBidException()
        {
        }

        public PlaceOrderBidException(string? message) : base(message)
        {
        }

        public PlaceOrderBidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PlaceOrderBidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
