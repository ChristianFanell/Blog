using FoodBlogApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ForumPost> ForumPosts { get; set; }

        public DbSet<Photo> Photos { get; set; }
    }
}
