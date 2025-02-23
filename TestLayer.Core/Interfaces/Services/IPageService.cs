using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Core.Entities;

namespace TestLayer.Core.Interfaces.Services
{
    public interface IPageService
    {
        Task<PageEntity> GetPageById(int id);
        Task<IEnumerable<PageEntity>> GetPagesAsync();
        Task<IEnumerable<PageEntity>> GetPageByUsernameAsync(string username);
        Task AddPageAsync(PageEntity entity);
        Task UpdatePageAsync(PageEntity entity);
        Task DeletePageAsync(int id);

    }
}
