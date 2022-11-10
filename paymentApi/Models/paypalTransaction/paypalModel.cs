using System.Xml.Linq;

namespace paymentApi.Models.paypalTransaction
{
    public class paypalModel
    {
        public nameModel name { get; set; }
        public string email_address { get; set; }
        public string account_id { get; set; }
    }
}
