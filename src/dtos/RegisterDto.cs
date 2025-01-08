using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [Required]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "La contraseña debe tener entre 1 y 6 caracteres.")]
        [RegularExpression(@"^(?=.*\d).+$", ErrorMessage = "La contraseña debe tener al menos un número.")]
        public string? Password { get; set; } = string.Empty;
    }
}