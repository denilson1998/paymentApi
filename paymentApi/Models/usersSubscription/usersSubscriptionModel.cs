using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace paymentApi.Models.usersPayment
{
    
    public class usersSubscriptionModel
    {
        [Key]
        public int userId { get; set; }

        public int subscriptionId { get; set; } 

    }
}
