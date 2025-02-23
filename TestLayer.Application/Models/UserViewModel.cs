using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Core.Entities;

namespace TestLayer.Application.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? FullName { get; set; }

    }
}
