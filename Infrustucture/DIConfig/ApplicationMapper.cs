using Application.DTOs.Account;
using Application.DTOs.Payment;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustucture.DIConfig
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
           CreateMap<PaymentDTO,Payment>().ReverseMap();
        }
    }
}
