using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Application.Models;
using TestLayer.Core.Entities;

namespace TestLayer.Application.Mappers
{
    public static class PageMapper
    {
        public static PageViewModel MapToViewModel(PageEntity entity)
        {
            return new PageViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                Likes = entity.Likes?.Count ?? 0,
                CreatedBy = entity.CreatedBy?.Username ?? "Unknown",
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
