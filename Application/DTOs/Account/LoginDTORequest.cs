using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class LoginDTORequest
    {
        public string? UsernameOrEmail {  get; set; }

        public string? Password { get; set; }    
    }
}
