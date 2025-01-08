using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.data;
using api.src.dtos;
using api.src.interfaces;
using api.src.models;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.src.repositories
{
    public class PostRespository : IPostInterface
    {

        private readonly DataContext _context;

        public PostRespository(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
        }

        public async Task CreatePostAsync(string userId, NewPostDto newPost, ImageUploadResult uploadResult)
        {
            if (newPost == null)
            {
                throw new ArgumentNullException(nameof(newPost));
            }

            if (uploadResult == null)
            {
                throw new ArgumentNullException(nameof(uploadResult));
            }

            var post = new Post
            {
                Title = newPost.Title,
                Publication_date = newPost.Publication_date,
                ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                AuthorId = userId,
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> GetPostAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            return new OkObjectResult(posts);
        }
    }
}