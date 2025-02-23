using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestLayer.Application.Mappers;
using TestLayer.Application.Models;
using TestLayer.Core.Interfaces.Services;

namespace TestLayer.Web.ViewComponents
{
    public class ProfileListPagesViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IPageService _pageService;

        public ProfileListPagesViewComponent(IUserService userService, IPageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var username = await GetUsernameAsync(id);
                if (string.IsNullOrEmpty(username))
                {
                    ViewBag.error = "Please sign in to see your profile.";
                    return View("error");
                }

                var pages = await _pageService.GetPageByUsernameAsync(username);
                var pagesvm = pages.Select(PageMapper.MapToViewModel).ToList();

                return id == 0 ? View("Admin", pagesvm) : View(pagesvm);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;

                return View("error");
            }
        }

        private async Task<string?> GetUsernameAsync(int id)
        {
            if (id == 0)
            {
                return HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            }

            var user = await _userService.GetUserByIdAsync(id);
            return user?.Username;
        }

    }
}
