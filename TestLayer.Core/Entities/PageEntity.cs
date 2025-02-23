using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Core.Entities
{

    public class PageLikeEntity
    {
        [Key]
        public int Id { get; set; }
        public int PageId { get; set; }
        public required PageEntity Page { get; set; }

        public int UserId { get; set; }
        public required UserEntity User { get; set; }
    }

    public class PageEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }

        public List<PageLikeEntity>? Likes { get; set; }

        public int UserId { get; set; }
        public required UserEntity CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
