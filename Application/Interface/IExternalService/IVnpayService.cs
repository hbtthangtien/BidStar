using Application.DTOs.Vnpay;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IExternalService
{
    public interface IVnpayService
    {
        public string CreatePaymentUrl(HttpContext context, VnpayDTORequest request);

        public ResponseDTOVnpay PaymentExcute(IQueryCollection collections);
    }
}
