using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.models;

namespace api.src.interfaces
{
    public interface ItokenService
    {
        string CreateToken(AppUser user);
    }
}