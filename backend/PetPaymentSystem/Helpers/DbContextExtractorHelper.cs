using Microsoft.EntityFrameworkCore.Internal;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Helpers
{
    public static class DbContextExtractorHelper
    {
        public static PaymentSystemContext Extract(DbContextPool<PaymentSystemContext> dbContext)//do not use DbContextPool anymore due its fucking cache policy
        {
            return dbContext.Rent();//Using non-public API because public API does not exist
        }
    }
}
