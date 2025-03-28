using Application.DTOs.Vnpay;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
       
    }
}
