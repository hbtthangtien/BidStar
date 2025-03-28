using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Vnpay
{
    public class ResponseDTOVnpay
    {
        public string vnp_TmnCode { get; set; }
        public double vnp_Amount { get; set; } = 0!;
        public string vnp_BankCode { get; set; }
        public string? vnp_BankTranNo { get; set; }
        public string? vnp_CardType { get; set; }
        public DateTime? vnp_PayDate { get; set; }
        public string vnp_OrderInfo { get; set; }
        public string vnp_TransactionNo { get; set; }
        public string vnp_ResponseCode { get; set; }
        public string vnp_TransactionStatus { get; set; }
        public string vnp_TxnRef { get; set; }
        public string? vnp_SecureHashType { get; set; }
        public string vnp_SecureHash { get; set; }
        public bool Succes { get; set; }
        public string message { get; set; }
        public string InvoiceId { get; set; }
    }
}
