using Microsoft.EntityFrameworkCore;
using paymentApi.Models.typeOfPayment;
using paymentApi.Models.typeOfSubscription;
using paymentApi.Models.usersPayment;

namespace paymentApi.Data
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<usersSubscriptionModel> usersSubscription { get; set; }
        public DbSet<typeOfPaymentModel> typesOfPayments { get; set; }

        public DbSet<typeOfSubscriptionModel> typesOfSubscriptions { get; set; }
    }
}
