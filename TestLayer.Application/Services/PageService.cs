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
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;

        public PageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public async Task AddPageAsync(PageEntity entity)
        {
            try
            {
                await _pageRepository.AddAsync(entity);
            }
            catch(Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }
            
        }

        public async Task DeletePageAsync(int id)
        {         
            try
            {
                await _pageRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"Page with id '{id}' not found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }
        }

        public async Task<PageEntity> GetPageById(int id)
        {
            try
            {
                return await _pageRepository.GetByIdAsync(id);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"Page with id '{id}' not found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }           
        }

        public async Task<IEnumerable<PageEntity>> GetPageByUsernameAsync(string username)
        {
            try
            {
                return await _pageRepository.GetPagesByUsername(username);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"No pages found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }
        }

        public async Task<IEnumerable<PageEntity>> GetPagesAsync()
        {
            try
            {
                return await _pageRepository.GetAllAsync();
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"No pages found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }           
        }

        public async Task UpdatePageAsync(PageEntity entity)
        {
            try
            {
                await _pageRepository.UpdateAsync(entity);
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
    }
}
