using Microsoft.AspNetCore.Mvc;
using TestLayer.Core.Interfaces.Services;

namespace TestLayer.Web.Controllers.Mvc
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
