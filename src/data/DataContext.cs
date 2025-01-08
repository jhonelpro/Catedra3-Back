using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using api.src.models;
using Microsoft.EntityFrameworkCore;

namespace api.src.data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<AppUser> AppUsers { get; set; } = null!;

    }
}