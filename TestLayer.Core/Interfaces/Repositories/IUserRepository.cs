using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Application.DTOs;
using TestLayer.Core.Entities;

namespace TestLayer.Core.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity> GetByUsernameAsync(string username);
        Task<UserEntity> AuthUserAsync(UserLoginDto user);
        bool CheckHasPage(UserEntity entity, int pageId);
    }
}
