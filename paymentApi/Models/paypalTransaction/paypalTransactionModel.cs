using Microsoft.AspNetCore.Mvc.RazorPages;

namespace paymentApi.Models.paypalTransaction
{
    public class paypalTransactionModel
    {
        public string id { get; set; }
        public string status { get; set; }
        public paymentSourceModel payment_source { get; set; }
        public List<purchaseUnitModelT> purchase_units { get; set; }
        public payerModel payer { get; set; }
        public List<linkModel> links { get; set; }
    }
}
