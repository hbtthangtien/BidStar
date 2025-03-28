using Application.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IAuthenticateService
    {
        public Task AuthenticateCretial(LoginDTORequest request);

        public Task Logout();
    }
}
