using Application.DTOs.Payment;
using Application.DTOs.Vnpay;
using Application.Interface.IExternalService;
using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentService : Service, IPaymentService
    {
        private readonly IVnpayService _vnpayService;
        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IVnpayService vnpayService) : base(unitOfWork, mapper)
        {
            _vnpayService = vnpayService;
        }

        public async Task<string> CreatePaymentAsync(VnpayDTORequest request, HttpContext context)
        {
            await Task.CompletedTask;
            string paymentUrl = _vnpayService.CreatePaymentUrl(context,request);
            return paymentUrl;
        }

        public async Task ExecutePaymentAsync(IQueryCollection collections, string userId)
        {
            var response = _vnpayService.PaymentExcute(collections);
            var user =await _unitOfWork.Accounts.UserManager.FindByIdAsync(userId);
            if (response.Succes)
            {
                user!.Balance += response.vnp_Amount;
                await _unitOfWork.Accounts.UserManager.UpdateAsync(user);
            }
            var payment = new Payment
            {
                BuyerId = userId,
                Amount = response.vnp_Amount,
                Code = response.vnp_BankCode,
                Content = response.vnp_OrderInfo,
                DatePayment = DateTime.Now,
                PaymentStatus = (response.vnp_ResponseCode == "00") ? Domain.Enum.PaymentStatus.Paid : Domain.Enum.PaymentStatus.Failed,
                PaymentMethod = Domain.Enum.PaymentMethod.Deposit,
                
            };
            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<PaymentDTO>> GetAllPaymentByUserId(string userId)
        {
            var data = await _unitOfWork.Payments.FindAsync(e => e.BuyerId ==  userId);
            var list = _mapper.Map<List<PaymentDTO>>(data);
            return list;
        }
    }
}
