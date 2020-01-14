using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Helpers;
using PetPaymentSystem.Models.Generated;
using System;
using System.Linq;
using PetPaymentSystem.Exceptions;

namespace PetPaymentSystem.Services
{
    public class SessionManagerService
    {
        private readonly PaymentSystemContext _dbContext;
        private readonly ILogger<SessionManagerService> _logger;
        private const int SessionMinutesToExpire = 30;

        public SessionManagerService(PaymentSystemContext dbContext, ILogger<SessionManagerService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Session Create(Merchant merchant, SessionCreateRequest request)
        {
            try
            {
                if (_dbContext.Session.Any(x => x.MerchantId == merchant.Id && x.OrderId == request.OrderId))
                    throw new OuterException(InnerError.SessionAlreadyExists);

                var session = new Session
                {
                    Amount = request.Amount,
                    Currency = request.Currency,
                    FormKey = request.FormKey,
                    FormLanguage = request.FormLanguage,
                    MerchantId = merchant.Id,
                    OrderDescription = request.OrderDescription,
                    OrderId = request.OrderId,
                    ExternalId = IdHelper.GetSessionId(),
                    ExpireTime = DateTime.UtcNow.AddMinutes(SessionMinutesToExpire),
                    SessionType = request.SessionType,
                    TryCount = 0
                };

                _dbContext.Session.Add(session);
                _dbContext.SaveChanges();
                return session;
            }
            catch (DbUpdateException)
            {
                throw new OuterException(InnerError.SessionAlreadyExists);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong Merchant-[{merchant.Id}] OrderId[{request.OrderId}]");
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Session Get(string sessionId)
        {
            var session = _dbContext.Session.Include(x=>x.Operation).FirstOrDefault(x => x.ExternalId == sessionId);
            if (session == null) throw new OuterException(InnerError.SessionNotFound);
            return session;
        }
        public Session GetByOrderId(Merchant merchant, string orderId)
        {
            var session = _dbContext.Session.FirstOrDefault(x => x.OrderId == orderId && x.MerchantId == merchant.Id);
            if (session == null) throw new OuterException(InnerError.SessionNotFound);
            return session;
        }

    }
}
