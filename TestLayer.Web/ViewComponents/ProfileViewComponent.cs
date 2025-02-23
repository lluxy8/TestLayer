using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TestLayer.Application.Mappers;
using TestLayer.Core.Entities;
using TestLayer.Core.Interfaces.Services;

namespace TestLayer.Web.ViewComponents
{
    public class ProfileViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public ProfileViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var user = await GetUserAsync(id);
                if (user == null)
                {
                    ViewBag.error = "User not found.";
                    return View("error");
                }
                

                var uservm = UserMapper.MaptoViewModel(user);
                return id == 0 ? View("Admin", uservm) : View(uservm);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;

                return View("error");
            }
        }

        private async Task<UserEntity?> GetUserAsync(int id)
        {
            if (id == 0)
            {
                var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(username))
                    return null;

                return await _userService.GetUserByUsernameAsync(username);
            }

            return await _userService.GetUserByIdAsync(id);
        }

    }

}
