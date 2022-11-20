using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using paymentApi.Data;
using paymentApi.Models.responseModel;
using paymentApi.Models.typeOfSubscription;

namespace paymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class subscriptionsController : ControllerBase
    {
        private readonly dataContext _dataContext;


        public subscriptionsController(dataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet("getSubscriptions")]
        public async Task<JsonResult> getSubscriptions()
        {
            try
            {
                List<typeOfSubscriptionModel> respSubs = await _dataContext.typesOfSubscriptions.ToListAsync();

                responseModel resp = new responseModel { estado = 1, mensaje = "Lista de los Tipos de Subscripciones!", detalle = respSubs };
                
                return new JsonResult(resp);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
