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
    public class PageRepository : GenericRepository<PageEntity>, IPageRepository
    {
        private readonly AppDbContext _context;

        public PageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<IEnumerable<PageEntity>> GetAllAsync()
        {
            var pages = await _context.Pages
                .Include(p => p.CreatedBy)
                .Include(p => p.Likes)
                .ToListAsync()
                ?? throw new KeyNotFoundException();

            return pages;
        }

        public new async Task<PageEntity> GetByIdAsync(int id)
        {
            var page = await _context.Pages
                .Include(p => p.CreatedBy)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException();

            return page;
        }

        public async Task<IEnumerable<PageEntity>> GetPagesByUsername(string username)
        {
            var pages = await _context.Pages
                .Include(p => p.CreatedBy)
                .Include(p => p.Likes)
                .Where(p => p.CreatedBy.Username == username)
                .ToListAsync()
                ?? throw new KeyNotFoundException();

            return pages;
        }
    }
}
