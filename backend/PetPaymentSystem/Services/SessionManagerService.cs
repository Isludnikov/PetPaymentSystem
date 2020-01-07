using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Helpers;
using PetPaymentSystem.Models.Generated;
using System;
using System.Linq;

namespace PetPaymentSystem.Services
{
    public class SessionManagerService
    {
        private readonly PaymentSystemContext _dbContext;
        private readonly ILogger<SessionManagerService> _logger;

        public SessionManagerService(PaymentSystemContext dbContext, ILogger<SessionManagerService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public SessionCreateResponse Create(Merchant merchant, SessionCreateRequest request)
        {
            try
            {
                if (_dbContext.Session.Any(x => x.MerchantId == merchant.Id && x.OrderId == request.OrderId))
                    return CreateFailResponse();
                    
                var session = new Session
                {
                    Amount = request.Amount,
                    Currency = request.Currency,
                    FormKey = request.FormKey,
                    FormLanguage = request.FormLanguage,
                    MerchantId = merchant.Id,
                    OrderDescription = request.OrderDescription,
                    OrderId = request.OrderId,
                    ExternalId = IdHelper.GetSessionId()
                };

                _dbContext.Session.Add(session);
                _dbContext.SaveChanges();
                return new SessionCreateResponse {Session = session};
            }
            catch (DbUpdateException)
            {
                return CreateFailResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong Merchant-[{merchant.Id}] OrderId[{request.OrderId}]");
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public SessionCreateResponse Get(Merchant merchant, string sessionId)
        {
            var session =  _dbContext.Session.FirstOrDefault(x => x.ExternalId == sessionId && x.MerchantId == merchant.Id);
            return session != null ? new SessionCreateResponse {Session = session} : new SessionCreateResponse {InnerError = InnerError.SessionNotFound};
        }
        private SessionCreateResponse CreateFailResponse(InnerError innerError = InnerError.SessionAlreadyExists)
        {
            return new SessionCreateResponse { InnerError = innerError };
        }
    }
}
