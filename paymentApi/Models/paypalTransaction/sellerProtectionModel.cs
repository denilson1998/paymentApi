namespace paymentApi.Models.paypalTransaction
{
    public class sellerProtectionModel
    {
        public string status { get; set; }
        public List<string> dispute_categories { get; set; }
    }
}
