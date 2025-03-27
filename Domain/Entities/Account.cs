using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account  : IdentityUser
    {

        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public double? Balance { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual Buyer? Buyer { get; set; }
    }
}
