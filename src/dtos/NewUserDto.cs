using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.dtos
{
    public class NewUserDto
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}