using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Security.Claims;
using TestLayer.Application.Mappers;
using TestLayer.Application.Services;
using TestLayer.Core.Interfaces.Services;

namespace TestLayer.Web.ViewComponents
{
    public class PageContentViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;
        private readonly IUserService _userService;

        public PageContentViewComponent(IPageService pageService, IUserService userService)
        {
            _pageService = pageService;
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var page = await _pageService.GetPageById(id);
                var pagevm = PageMapper.MapToViewModel(page);

                var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                ViewBag.liked = false;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = await _userService.GetUserByUsernameAsync(username);

                    if (user != null)
                    {
                        if (user.LikedPages?.Find(x => x.Id == id) != null)
                            ViewBag.liked = true;

                        bool hasPage = await _userService.CheckUserHasPage(user.Id, id);
                        if (hasPage)
                            return View("Admin", pagevm);
                    }
                    else { ViewBag.liked = true; }
                }
                else { ViewBag.liked = true; }
                
                return View(pagevm);

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("error");
            }

            
        }
    }
}
