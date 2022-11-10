namespace paymentApi.Models.respOrderCreated
{
    public class respOrderCreatedModel
    {
        public string id { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public List<linksModel>? links { get; set; }
    }
}
