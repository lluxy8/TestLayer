using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Core.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<PageEntity>? Pages { get; set; }
        public List<PageLikeEntity>? LikedPages { get; set; }
    }

}
