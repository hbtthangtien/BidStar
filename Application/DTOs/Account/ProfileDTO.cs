using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class ProfileDTO
    {
        public string? Email {  get; set; }

        public string? Address { get; set; }

        public double Balance { get; set; } 

        public string? Username { get; set; }

    }
}
