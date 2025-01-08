using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.src.data;
using api.src.interfaces;
using api.src.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api.src.services
{
    public class TokenService : ItokenService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SymmetricSecurityKey _key;

        public TokenService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            var signingKey = "EstaEsUnaClaveDeFirmaSuficientementeLargaQueSuperaLos64Bytes1234567890";

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }

        public string CreateToken(AppUser user)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!), 
                new Claim(ClaimTypes.NameIdentifier, user.Id), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
            };

            var userRoles = _userManager.GetRolesAsync(user);

            foreach (var role in userRoles.Result)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            
            var issuer = "http://localhost:5000" ?? throw new ArgumentNullException("JWT Issuer cannot be null or empty.");;

            var audience = "http://localhost:5000" ?? throw new ArgumentNullException("JWT Audience cannot be null or empty.");
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), 
                Expires = DateTime.Now.AddDays(1), 
                SigningCredentials = creds, 
                Issuer = issuer, 
                Audience = audience 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}