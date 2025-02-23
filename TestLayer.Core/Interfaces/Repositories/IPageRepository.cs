using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Core.Entities;

namespace TestLayer.Core.Interfaces.Repositories
{
    public interface IPageRepository : IGenericRepository<PageEntity>
    {
        Task<IEnumerable<PageEntity>> GetPagesByUsername(string username);
    }
}
