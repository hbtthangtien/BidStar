using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ExceptionCustom
{
    public class CreateException : Exception
    {
        public CreateException()
        {
        }
        public CreateException(IEnumerable<IdentityError> errors) : base(string.Join(";", errors.Select(e =>$"{e.Description}")))
        {
            
        }
        public CreateException(string? message) : base(message)
        {
        }

        public CreateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
