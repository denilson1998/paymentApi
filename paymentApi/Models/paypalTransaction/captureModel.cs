using paymentApi.Models.paypalOrder;

namespace paymentApi.Models.paypalTransaction
{
    public class captureModel
    {
        public string id { get; set; }
        public string status { get; set; }
        public AmountModel  amount { get; set; }
        public sellerProtectionModel seller_protection { get; set; }
        public bool final_capture { get; set; }
        public string disbursement_mode { get; set; }
        public sellerReceivableBreakdownModel seller_receivable_breakdown { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public List<linkModel> links { get; set; }
    }
}
