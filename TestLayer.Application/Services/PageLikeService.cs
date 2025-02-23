using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Application.Helpers.HttpResponseExceptions;
using TestLayer.Core.Entities;
using TestLayer.Core.Interfaces.Repositories;
using TestLayer.Core.Interfaces.Services;

namespace TestLayer.Application.Services
{
    public class PageLikeService : IPageLikeService
    {
        private readonly IPageLikeRepository _pageLikeRepository;

        public PageLikeService(IPageLikeRepository pageLikeRepository)
        {
            _pageLikeRepository = pageLikeRepository;
            
        }
        public async Task AddPageLikeAsync(PageLikeEntity entity)
        {
            try
            {
                await _pageLikeRepository.AddAsync(entity);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"Page not found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }
        }

        public async Task<IEnumerable<PageLikeEntity>> GetUserLikesAsync(int userId)
        {
            try
            {
                return await _pageLikeRepository.GetUserLikedPages(userId);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"Page not found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }
        }

        public Task RemovePageLikeAsync(int likeId)
        {
            throw new NotImplementedException();
        }
    }
}
