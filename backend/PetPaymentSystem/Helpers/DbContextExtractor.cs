using Microsoft.EntityFrameworkCore.Internal;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Helpers
{
    public static class DbContextExtractor
    {
        public static PaymentSystemContext Extract(DbContextPool<PaymentSystemContext> dbContext)
        {
            return dbContext.Rent();//Using non-public API because public API does not exist
        }
    }
}
