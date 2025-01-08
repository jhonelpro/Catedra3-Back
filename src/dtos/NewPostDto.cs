using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.src.models;
using Newtonsoft.Json.Serialization;

namespace api.src.dtos
{
    public class NewPostDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "El título debe tener al menos 5 caracteres.")]
        public string Title { get; set; } = string.Empty;
        public DateOnly Publication_date { get; set; }
        public IFormFile Image { get; set; } = null!;
    }
}