using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ExceptionCustom
{
    public class LoginException : Exception
    {
        public LoginException()
        {
        }
        public LoginException(IEnumerable<IdentityError> errors) : base(string.Join("\n",errors.Select(e => $"{e.Description}")))
        {
            
        }
        public LoginException(string? message) : base(message)
        {
        }

        public LoginException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LoginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
