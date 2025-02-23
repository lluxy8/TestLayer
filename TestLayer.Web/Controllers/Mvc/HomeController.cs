using Microsoft.AspNetCore.Mvc;

namespace TestLayer.Web.Controllers.Mvc
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
