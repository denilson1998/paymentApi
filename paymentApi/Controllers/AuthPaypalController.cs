using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using paymentApi.Models;
using paymentApi.Models.paypalOrder;
using paymentApi.Models.responseModel;
using paymentApi.Models.respOrderCreated;
using paymentApi.Models.paypalTransaction;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace paymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthPaypalController : ControllerBase
    {
        private IConfiguration configuration;

        public AuthPaypalController(IConfiguration IConf)
        {
            this.configuration = IConf;
        }

        [HttpPost("post")]
        public async Task<JsonResult> post(/*string start_date, string end_date*/)
        {
            try
            {
                const SecurityProtocolType tls = (SecurityProtocolType)12288;
                ServicePointManager.SecurityProtocol = tls | SecurityProtocolType.Tls12;

                tokenJsonModel token = new tokenJsonModel();

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("en_US"));

                    var clientId = configuration.GetValue<string>("PaypalCredentials:clientId");
                    var clientSecret = configuration.GetValue<string>("PaypalCredentials:clientSecret");
                    var bytes = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

                    var keyValues = new List<KeyValuePair<string, string>>();

                    keyValues.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                    var respMsg = await client.PostAsync("https://api.sandbox.paypal.com/v1/oauth2/token", new FormUrlEncodedContent(keyValues));
                    var resp = await respMsg.Content.ReadAsStringAsync();

                    token = JsonConvert.DeserializeObject<tokenJsonModel>(resp);

                }
                if (token != null)
                {
                    //var transactionHistoryUrl = "https://api-m.sandbox.paypal.com/v1/reporting/transactions?start_date=" + start_date + "&end_date=" + end_date + "&fields=all&pages"
                    return new JsonResult(token);
                }
                return new JsonResult("Pleasy Try again something getting problem!");    
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("paypalCreateOrder")]
        public async Task<JsonResult> paypalCreateOrder(string priceSubscription)
        {
            try
            {
                bool status = false;
                //string resp = string.Empty;
                responseModel resp = new responseModel();
                string approvalId = string.Empty;

                using (HttpClient client = new HttpClient())
                {
                    var clientId = configuration.GetValue<string>("PaypalCredentials:clientId");
                    var clientSecret = configuration.GetValue<string>("PaypalCredentials:clientSecret");
                    
                    var authToken = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");

                    client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                    var order = new paypalOrderModel()
                    {
                        intent = "CAPTURE",
                        purchase_units = new List<purchaseUnitModel>()
                        {
                            new purchaseUnitModel()
                            {
                                amount = new AmountModel()
                                {
                                    currency_code = "USD",
                                    value = priceSubscription
                                }
                            }
                        },
                        application_context = new applicationContextModel()
                        {
                            brand_name = "subscriptionApi",
                            landing_page = "LOGIN",
                            shipping_preference = "NO_SHIPPING",
                            user_action = "PAY_NOW",
                            return_url = "https://youtube.com",
                            cancel_url = "https://google.com"
                        }
                    };
                    var respJson = JsonConvert.SerializeObject(order);

                    var data = new StringContent(respJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);

                    status = response.IsSuccessStatusCode;

                    if (status)
                    {

                        //var respOrderModel = JsonConvert.SerializeObject(resp);
                        respOrderCreatedModel respOrderCreated = JsonConvert.DeserializeObject<respOrderCreatedModel>(response.Content.ReadAsStringAsync().Result);

                        linksModel linkApproved = new linksModel();

                        foreach (linksModel item in respOrderCreated.links)
                        {
                            if (item.rel == "approve")
                            {
                                linkApproved.href = item.href;
                                linkApproved.rel = item.rel;
                                linkApproved.method = item.method;
                            }
                        }
                        Uri url = new Uri(linkApproved.href);

                        string getTokenApproved = HttpUtility.ParseQueryString(url.Query).Get("token");

                        resp.estado = 1;
                        resp.mensaje = "Orden Creada Con Exito!";
                        resp.detalle = linkApproved;


                        return new JsonResult(resp);
                    }
                    else
                    {
                        resp.estado = 1;
                        resp.mensaje = "Error al Crear la Orden!";
                        resp.detalle = null;

                        return new JsonResult(resp);
                    }
                    
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("paypalApproveOrder")]
        public async Task<JsonResult> paypalApproveOrder(string token)
        {
            try
            {
                bool status = false;
                responseModel resp = new responseModel();
                using (HttpClient client = new HttpClient())
                {
                    var clientId = configuration.GetValue<string>("PaypalCredentials:clientId");
                    var clientSecret = configuration.GetValue<string>("PaypalCredentials:clientSecret");

                    var authToken = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");

                    client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                    var data = new StringContent("{}", Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"/v2/checkout/orders/{token}/capture", data);

                    status = response.IsSuccessStatusCode;

                    if (status)
                    {

                        //var respOrderModel = JsonConvert.SerializeObject(resp);
                        paypalTransactionModel respOrderCreated = JsonConvert.DeserializeObject<paypalTransactionModel>(response.Content.ReadAsStringAsync().Result);


                        resp.estado = 1;
                        resp.mensaje = "Pago Exitoso!";
                        resp.detalle = respOrderCreated;


                        return new JsonResult(resp);
                    }
                    else
                    {
                        resp.estado = 1;
                        resp.mensaje = "Error al pagar!";
                        resp.detalle = null;

                        return new JsonResult(resp);
                    }

                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
