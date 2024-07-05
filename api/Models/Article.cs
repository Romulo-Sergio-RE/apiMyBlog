using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
        public string Content { get; set; } = string.Empty;
        public string TimeRead { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int? UserIdAdmin { get; set; } = string.Empty;
        public User? UserAdimin { get; set; }
        public List<Comment> Comments { get; sert;} = new List<Comment>();
    }
}