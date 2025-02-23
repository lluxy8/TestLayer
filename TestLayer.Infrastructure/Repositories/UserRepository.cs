using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Application.DTOs;
using TestLayer.Core.Entities;
using TestLayer.Core.Interfaces.Repositories;
using TestLayer.Infrastructure.Data;

namespace TestLayer.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserEntity> AuthUserAsync(UserLoginDto user)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x =>
                x.Username == user.Username && x.Password == user.Password) 
                ?? throw new KeyNotFoundException();

            return entity;
        }

        public async Task<UserEntity> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username)
                ?? throw new KeyNotFoundException();

            return user;
        }

        public bool CheckHasPage(UserEntity entity, int pageId)
        {
            return entity.Pages?.Any(x => x.Id == pageId) ?? false;
        }

    }
}
