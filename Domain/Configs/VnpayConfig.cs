using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configs
{
    public class VnpayConfig
    {
        public string vnp_TmnCode { get; set; }
        public string vnp_HashSecret { get; set; }
        public string vnp_Url { get; set; }
        public string vnp_ReturnUrl { get; set; }
        public string vnp_Version { get; set; }
        public string vnp_Command { get; set; }
        public string vnp_CurrCode { get; set; }
        public string vnp_locale { get; set; }
    }
}
