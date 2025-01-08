using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.dtos;
using api.src.interfaces;
using api.src.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ItokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, ItokenService tokenService , SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (await _userManager.Users.AnyAsync(p => p.Email == registerDto.Email)) return BadRequest( new { message = "El correo ya existe"});


                var user = new AppUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                };
                
                if (string.IsNullOrEmpty(registerDto.Password))
                {
                    return BadRequest(new { message = "Password cannot be null or empty" });
                }

                var createUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");

                    if (roleResult.Succeeded) {
                        return Ok(new NewUserDto
                        {
                            Email = user.Email!,
                            Token = _tokenService.CreateToken(user)
                        });
                    }
                    else
                    {
                        return StatusCode(500, new { message = roleResult.Errors});
                    }
                }else
                {
                    return StatusCode(500,  new { message = createUser.Errors});
                }

            } 
            catch (Exception e)
            {
                return StatusCode(500,  new { message = e.Message});
            }
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterDto loginDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try {
                
                if (User.Identity?.IsAuthenticated == true)
                {
                    return BadRequest(new { message = "Sesion activa" });
                }

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
                if(user == null) return Unauthorized( new { message = "Correo o contraseña incorrectos."});

                if (string.IsNullOrEmpty(loginDto.Password)) return BadRequest(new { message = "Password cannot be null or empty" });

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if(!result.Succeeded) return Unauthorized( new { message = "Correo o contraseña incorrectos."});

                await _signInManager.SignInAsync(user, isPersistent: true);

                var token = _tokenService.CreateToken(user);

                if (string.IsNullOrEmpty(token)) return Unauthorized( new { message = "Token invalido"});

                return Ok(
                    new NewUserDto
                    {
                        Email = user.Email!,
                        Token = token
                    }
                );

            } catch (Exception ex) {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}