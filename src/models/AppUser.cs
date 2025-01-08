using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.src.models
{
    public class AppUser : IdentityUser
    {
        public override string? Email { get; set; } = string.Empty;
    }
}