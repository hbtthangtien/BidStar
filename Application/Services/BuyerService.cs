﻿using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BuyerService : Service, IBuyerService
    {
        public BuyerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public Task<Buyer> GetByUserNameAsync(string? buyerId)
        {
            throw new NotImplementedException();
        }
    }
}
