using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Core.Entities;

namespace TestLayer.Core.Interfaces.Services
{
    public interface IPageLikeService
    {
        Task AddPageLikeAsync(PageLikeEntity entity);
        Task RemovePageLikeAsync(int likeId);
        Task<IEnumerable<PageLikeEntity>> GetUserLikesAsync(int userId);

    }
}
