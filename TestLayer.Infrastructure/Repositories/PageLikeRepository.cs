using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Core.Entities;
using TestLayer.Core.Interfaces.Repositories;
using TestLayer.Infrastructure.Data;

namespace TestLayer.Infrastructure.Repositories
{
    public class PageLikeRepository : GenericRepository<PageLikeEntity>, IPageLikeRepository
    {
        private readonly AppDbContext _context;
        public PageLikeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PageLikeEntity>> GetUserLikedPages(int userId)
        {
            var user = await _context.Users
                .Include(u => u.LikedPages)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new KeyNotFoundException();

            return user.LikedPages?.ToList(); 
        }
    }
}
