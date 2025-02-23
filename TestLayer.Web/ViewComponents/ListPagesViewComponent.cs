using Microsoft.AspNetCore.Mvc;
using TestLayer.Application.Mappers;
using TestLayer.Application.Models;
using TestLayer.Core.Interfaces.Services;

namespace TestLayer.Web.ViewComponents
{
    public class ListPagesViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        public ListPagesViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var pages = await _pageService.GetPagesAsync();
                var pagesvm = pages.Select(PageMapper.MapToViewModel).ToList();

                return View(pagesvm);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
            }

            return View();
        }
    }
}
