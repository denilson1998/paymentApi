namespace paymentApi.Models.paypalTransaction
{
    public class sellerReceivableBreakdownModel
    {
        public grossAmountModel gross_amount { get; set; }
        public paypalFeeModel paypal_fee { get; set; }
        public netAmountModel net_amount { get; set; }
    }
}
