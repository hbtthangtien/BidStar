using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constant
{
    public class LinkConstant
    {
        private static string baseUri = $"https://localhost:7098";

        public static UriBuilder UriBuilder(string userId, string token, string path)
        {
            var builder = new UriBuilder(baseUri);
            builder.Path = $"account/{path}";
            builder.Query = $"userId={userId}&token={Uri.EscapeDataString(token)}";
            return builder;
        }
    }
}
