using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Application.DTOs;
using TestLayer.Core.Entities;

namespace TestLayer.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task AddUserAsync(UserEntity entity);
        Task<UserEntity> GetUserByUsernameAsync(string username);
        Task<IEnumerable<UserEntity>> GetUsersAsync();
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<bool> CheckUserHasPage(int userId, int pageId);
        Task UpdateUserAsync(UserEntity entity);
        Task DeleteUserAsync(int id);
        Task AuthUserAsync(UserLoginDto user);

    }
}
