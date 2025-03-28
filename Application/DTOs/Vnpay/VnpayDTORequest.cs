using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Vnpay
{
    public class VnpayDTORequest
    {
        public double Amount { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
