using Application.DTOs.Payment;
using Application.DTOs.Vnpay;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IPaymentService
    {
        public Task<List<PaymentDTO>> GetAllPaymentByUserId(string userId);

        public Task<string> CreatePaymentAsync(VnpayDTORequest request, HttpContext context);

        public Task ExecutePaymentAsync(IQueryCollection collections, string userId);
    }
}
