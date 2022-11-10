namespace paymentApi.Models.paypalOrder
{
    public class paypalOrderModel
    {
        public string intent { get; set; }
        public List<purchaseUnitModel> purchase_units { get; set; }
        public applicationContextModel application_context { get; set; }
  
    }
}
