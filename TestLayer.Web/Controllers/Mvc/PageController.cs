using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TestLayer.Application.Services;
using TestLayer.Core.Entities;
using TestLayer.Core.Interfaces.Services;

namespace TestLayer.Web.Controllers.Mvc
{
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IUserService _userService;
        private readonly IPageLikeService _pageLikeService;

        public PageController(IPageService pageService, IUserService userService, IPageLikeService pageLikeService)
        {
            _pageService = pageService;
            _userService = userService;
            _pageLikeService = pageLikeService;
        }

        public IActionResult Index(int id)
        {
            if(id == 0)
                RedirectToAction("pages", "CreatePage");

            ViewBag.id = id;
            return View();
        }

        public IActionResult Pages()
        {
            return View();
        }

        [Authorize]
        public IActionResult CreatePage()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePage(PageEntity entity)
        {
            try
            {
                var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                var user = await GetUserAsync();

                if(user == null)
                    return RedirectToAction("Login", "Account");

                entity.CreatedBy = user;

                await _pageService.AddPageAsync(entity);
                return RedirectToAction("Index", "Page");
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdatePage(PageEntity entity)
        {
            try
            {
                var user = await GetUserAsync();
                var page = await _pageService.GetPageById(entity.Id);

                if (user == null || !await _userService.CheckUserHasPage(user.Id, entity.Id))
                    return RedirectToAction("Login", "Account");

                page.Content = entity.Content;
                page.Title = entity.Title;

                await _pageService.UpdatePageAsync(page);

                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                //TODO: daha detaylı olabilir
                return RedirectToAction("Index", "Profile");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeletePage(int id)
        {
            try
            {
                var user = await GetUserAsync();
                await _pageService.GetPageById(id);

                if (user == null || !await _userService.CheckUserHasPage(user.Id, id))
                    return RedirectToAction("Login", "Account");

                await _pageService.DeletePageAsync(id);

                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                //TODO: daha detaylı olabilir
                return RedirectToAction("Index", "Profile");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LikePage(int pageId)
        {
            var user = await GetUserAsync();
            var page = await _pageService.GetPageById(pageId);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var like = new PageLikeEntity { Page = page, User = user };

            if (user.LikedPages?.Find(x => x.Id == pageId) != null)
            {
                await _pageLikeService.AddPageLikeAsync(like);
            }

            return RedirectToAction("Pages", "Page", new { id = pageId });
        }


        private async Task<UserEntity?> GetUserAsync()
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
                return null;

            return await _userService.GetUserByUsernameAsync(username);
        }
    }
}
