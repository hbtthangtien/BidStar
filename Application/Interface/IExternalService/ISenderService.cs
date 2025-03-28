using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IExternalService
{
    public interface ISenderService
    {
        public Task SendMailAsync(string Subject, string Body, string Email);
    }
}
