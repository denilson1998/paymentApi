using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace paymentApi.Models.typeOfSubscription
{
    public class typeOfSubscriptionModel
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; } = string.Empty;
        public int price { get; set; }

    }
}
