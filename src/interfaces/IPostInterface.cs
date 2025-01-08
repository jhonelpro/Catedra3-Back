using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.dtos;
using api.src.models;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace api.src.interfaces
{
    public interface IPostInterface
    {
        Task CreatePostAsync(string userId, NewPostDto newPost, ImageUploadResult uploadResult);
        Task<IActionResult> GetPostAsync();
    }
}