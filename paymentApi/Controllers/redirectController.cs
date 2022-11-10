using Microsoft.AspNetCore.Mvc;

namespace paymentApi.Controllers
{
    public class redirectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
