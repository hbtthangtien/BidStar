using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Payment
{
    public class PaymentDTO
    {
        public double Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string Content { get; set; }

        public string Code { get; set; }

        public DateTime DatePayment { get; set; }

    }
}
