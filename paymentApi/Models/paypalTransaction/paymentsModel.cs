using System.Text.RegularExpressions;

namespace paymentApi.Models.paypalTransaction
{
    public class paymentsModel
    {
        public List<captureModel> captures { get; set; }
    }
}
