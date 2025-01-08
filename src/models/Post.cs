using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateOnly Publication_date { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public AppUser Author { get; set; } = null!;
    }
}