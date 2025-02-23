using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Application.DTOs;
using TestLayer.Application.Models;
using TestLayer.Core.Entities;

namespace TestLayer.Application.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel MaptoViewModel(UserEntity entity)
        {
            return new UserViewModel
            {
                Id = entity.Id,
                Username = entity.Username,
                CreatedAt = entity.CreatedAt,
                FullName = $"{entity.Name} {entity.Surname}",
            };
        }

        public static UserEntity MapUserSignUpToEntity(UserSignUpDto dto)
        {
            return new UserEntity
            {
                Username = dto.Username,
                Name = dto.Name,
                Surname = dto.Surname,
                Password = dto.Password,
                Email = dto.Email
            };
        }
    }
}
