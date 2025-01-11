using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.dtos;
using api.src.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using api.src.models;

namespace api.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostInterface _postInterface;
        private readonly Cloudinary _cloudinary;
        private readonly UserManager<AppUser> _userManager;

        public PostController(IPostInterface postInterface, Cloudinary cloudinary, UserManager<AppUser> userManager)
        {
            _postInterface = postInterface;
            _cloudinary = cloudinary;
            _userManager = userManager;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreatePostAsync(NewPostDto newPost)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (newPost.Image.ContentType != "image/jpg" && newPost.Image.ContentType != "image/png")
                {
                    return BadRequest( new { message = "La imagen debe ser un archivo jpg o png"});
                }

                if (newPost.Image.Length > 5 * 1024 * 1024)
                {
                    return BadRequest( new { message = "La imagen debe tener menos de 5 MB"});
                }

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(newPost.Image.FileName, newPost.Image.OpenReadStream()),
                    Folder = "E-commerce-Products"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    return BadRequest( new { message = uploadResult.Error.Message});
                }

                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return BadRequest( new { message = "Usuario no encontrado"});
                }

                await _postInterface.CreatePostAsync(user.Id, newPost, uploadResult);
                return Ok(new { message = "Post creado con éxito"});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPostAsync()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var post = await _postInterface.GetPostAsync();
                return Ok(post);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}