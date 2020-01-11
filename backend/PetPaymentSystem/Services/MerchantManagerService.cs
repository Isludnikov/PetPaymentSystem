using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetPaymentSystem.Cache;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Services
{
    public class MerchantManagerService
    {
        private readonly PaymentSystemContext _dbContext;
        private readonly bool _useCache;
        public MerchantManagerService(PaymentSystemContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _useCache = configuration.GetSection("Caching").GetValue<bool>("Merchants");
        }
        public Merchant GetMerchant(string token) =>
            _useCache
                ? MerchantCache.Get(token, _dbContext)
                : _dbContext.Merchant.Include(i=>i.MerchantIpRange).FirstOrDefault(x => x.Token == token);

        public Merchant GetMerchant(int id) =>
            _useCache
                ? MerchantCache.Get(id, _dbContext)
                : _dbContext.Merchant.Include(i=>i.MerchantIpRange).FirstOrDefault(x => x.Id == id);
    }
}
