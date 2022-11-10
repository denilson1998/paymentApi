namespace paymentApi.Models.paypalTransaction
{
    public class purchaseUnitModelT
    {
        public string reference_id { get; set; }
        public shippingModel shipping { get; set; }
        public paymentsModel payments { get; set; }
    }
}
