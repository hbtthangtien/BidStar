using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IBuyerService
    {
        Task<Buyer> GetByUserNameAsync(string? buyerId);
    }
}
