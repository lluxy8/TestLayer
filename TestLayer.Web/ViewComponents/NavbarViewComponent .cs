using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TestLayer.Application.Mappers;
using TestLayer.Core.Entities;
using TestLayer.Core.Interfaces.Services;

namespace TestLayer.Web.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public NavbarViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                var user = await _userService.GetUserByUsernameAsync(username);

                var userViewModel = UserMapper.MaptoViewModel(user);

                return View("logined", userViewModel);
            }
            catch
            {
                return View();
            }
        }
    }
}
