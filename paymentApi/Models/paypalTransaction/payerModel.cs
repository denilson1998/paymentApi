using System.Xml.Linq;

namespace paymentApi.Models.paypalTransaction
{
    public class payerModel
    {
        public nameModel name { get; set; }
        public string email_address { get; set; }
        public string payer_id { get; set; }
    }
}
