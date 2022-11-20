using Microsoft.EntityFrameworkCore;

namespace paymentApi.Models.typeOfPayment
{
    [Keyless]
    public class typeOfPaymentModel
    {
        public int id { get; set; }
        public string description { get; set; } = string.Empty;
    }
}
