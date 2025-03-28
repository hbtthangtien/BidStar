using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class CreateAccountDTO
    {
        public  string UserName { get; set; }
        public  string Password { get; set; }
        public  string ConfirmPassword { get; set; }
        public  string Email {  get; set; }
        public UserRole UserRole { get; set; }
    }
}
