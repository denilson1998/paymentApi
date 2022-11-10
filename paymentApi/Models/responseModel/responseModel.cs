namespace paymentApi.Models.responseModel
{
    public class responseModel
    {
        public int estado { get; set; }
        public string mensaje { get; set; } = string.Empty;
        public object detalle { get; set; } = string.Empty;
    }
}
