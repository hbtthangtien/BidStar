using Application.DTOs.Vnpay;
using Application.Interface.IExternalService;
using Domain.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
namespace Application.Services
{
    public class VnpayService : IVnpayService
    {
        private readonly VnpayConfig _vnpayConfig;
        public VnpayService(IOptions<VnpayConfig> options)
        {
            _vnpayConfig = options.Value;
        }
        public string CreatePaymentUrl(HttpContext httpContext, VnpayDTORequest request)
        {
            var tick = DateTime.Now.Ticks.ToString();
            var vnpay = new VnpayLibrary();
            vnpay.AddRequestData("vnp_Version", _vnpayConfig.vnp_Version);
            vnpay.AddRequestData("vnp_Command", _vnpayConfig.vnp_Command);
            vnpay.AddRequestData("vnp_TmnCode", _vnpayConfig.vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (request.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", request.CreateDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(httpContext));
            if (!string.IsNullOrEmpty(_vnpayConfig.vnp_locale))
            {
                vnpay.AddRequestData("vnp_Locale", _vnpayConfig.vnp_locale);
            }
            else
            {
                vnpay.AddRequestData("vnp_Locale", "vn");
            }
            vnpay.AddRequestData("vnp_OrderInfo", "123");
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", _vnpayConfig.vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", $"{tick}");
            var paymentUrl = vnpay.CreateRequestUrl(_vnpayConfig.vnp_Url, _vnpayConfig.vnp_HashSecret);
            return paymentUrl;
        }

        public ResponseDTOVnpay PaymentExcute(IQueryCollection collections)
        {
            var vnpay = new VnpayLibrary();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }
            long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_OrderId = (vnpay.GetResponseData("vnp_TxnRef"));
            var vn_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            string InvoiceId = vnpay.GetResponseData("vnp_OrderInfo");
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _vnpayConfig.vnp_HashSecret);
            if (!checkSignature)
            {
                return new ResponseDTOVnpay
                {
                    Succes = false,
                    message = "An error occurred during processing. vnp_ResponseCode:" + vnp_ResponseCode,
                    vnp_TransactionStatus = vnp_TransactionStatus,
                };
            }
            return new ResponseDTOVnpay
            {
                Succes = true,
                vnp_Amount = vnp_Amount,
                vnp_TxnRef = orderId.ToString(),
                vnp_TransactionNo = vnpayTranId.ToString(),
                vnp_ResponseCode = vnp_ResponseCode,
                vnp_TransactionStatus = vnp_TransactionStatus,
                message = "The transaction was executed successfully. Thank you for using the service",
                InvoiceId = InvoiceId
            };
        }
    }
}
