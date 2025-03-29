using Application.DTOs.Account;
using Application.DTOs.AuctionSession;
using Application.DTOs.Category;
using Application.DTOs.Payment;
using Application.DTOs.Product;
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
            CreateMap<ProductDTO,Product>().ReverseMap();
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<ProductDTOCreate, Product>().ReverseMap();
            CreateMap<ProductDTODetail, Product>().ReverseMap();
            CreateMap<AuctionSessionDTO,AuctionSession>().ReverseMap();
            CreateMap<AuctionSessionDTODetail, AuctionSession>().ReverseMap();

        }
    }
}
