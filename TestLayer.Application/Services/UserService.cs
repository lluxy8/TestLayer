using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Application.Helpers;
using TestLayer.Core.Entities;
using TestLayer.Core.Interfaces.Services;
using TestLayer.Infrastructure.Repositories;
using TestLayer.Application.Helpers.HttpResponseExceptions;
using TestLayer.Application.DTOs;

namespace TestLayer.Application.Services
{
    public class UserService : IUserService
    {

        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(UserEntity entity)
        {
            try
            {
                await _userRepository.GetByUsernameAsync(entity.Username);

                throw new CustomException(HttpStatusCode.BadRequest, "User already exists.");
            }
            catch (KeyNotFoundException)
            {
                await _userRepository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An internal error occurred. {ex.Message}, {ex.InnerException?.Message}");
            }
        }

        public async Task AuthUserAsync(UserLoginDto user)
        {
            try
            {
                await _userRepository.AuthUserAsync(user);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User Not Found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An internal error occurred. {ex.Message}");
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"User with id '{id}' not found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            try
            {
                return await _userRepository.GetByIdAsync(id);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"User with id '{id}' not found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }
        }

        public async Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _userRepository.GetByUsernameAsync(username);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"User with username '{username}' not found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"No users found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }
        }

        public async Task UpdateUserAsync(UserEntity entity)
        {
            try
            {
                await _userRepository.UpdateAsync(entity);
            }
            catch (KeyNotFoundException)
            {
                throw new CustomException(HttpStatusCode.NotFound, $"No users found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.InternalServerError, $"An Internal error occurred. {ex.Message}");
            }

        }

        public async Task<bool> CheckUserHasPage(int userId, int pageId)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new CustomException(HttpStatusCode.NotFound, "User not found");

            return _userRepository.CheckHasPage(user, pageId);
        }
    }
}
