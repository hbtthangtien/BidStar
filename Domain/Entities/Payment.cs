using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; } 

        public required string BuyerId { get; set; }

        public int OrderId { get; set; }

        public double Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string? Content { get; set; }

        public string? Code { get; set; }

        public DateTime DatePayment { get; set; }

        public virtual Order? Order { get; set; }

        public virtual Buyer? Buyer { get; set; }

    }
}
