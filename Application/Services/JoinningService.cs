using Application.Interface.IServices;
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
    public class JoinningService : Service, IJoinningService
    {
        public JoinningService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task JoinningSession(string buyerId, int sessionId)
        {
            var user = await _unitOfWork.Accounts.UserManager.FindByIdAsync(buyerId);
            var session = await _unitOfWork.AuctionSessions.GetSingle(e => e.Id == sessionId);
            if (user?.Balance < session?.BaseBalance)
            {
                throw new Exception("You don't enough balance to join session");
            }
            else
            {
                var joinning = new Joinning { BuyerId = buyerId, AuctionSessionId = sessionId, TimeJoin = DateTime.Now };
                await _unitOfWork.Joinnings.AddAsync(joinning);
            }
        }
    }
}
