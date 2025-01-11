using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.dtos
{
    public class PostDto
    {
        public string Title { get; set; } = string.Empty;
        public DateOnly Publication_date { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Author { get; set; } = null!;
    }
}