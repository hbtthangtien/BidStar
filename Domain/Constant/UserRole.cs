using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constant
{
    public static class UserRole
    {
        public const string BUYER = "BUYER";

        public const string SELLER = "SELLER";

        public const string ADMIN = "ADMIN";

        public static readonly string[] ALL = [BUYER,SELLER];
    }
}
