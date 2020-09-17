using FoodBlogApi.Entities;
using FoodBlogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(ApplicationDbContext context/*, IServiceProvider services*/)
        {
            //var userMgr = services.GetRequiredService < UserManager<AppUser>>();
            //var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();

            var author = new Author { About = "Admin", Email = "christianfanell@icloud.com", FirstName = "Christian", LastName = "Fanell", NickName = "Admin" };
            

            //kollar om tabeller tomma, annars fyll på
            if (!context.Authors.Any())
            {
                context.Authors.Add(author);
                context.SaveChanges();
            }

            //if (!context.ForumPosts.Any())
            //{
            //    context.ForumPosts.RemoveRange(
            //    context.ForumPosts.Where(c => c.AuthorId > 0));
            //    context.SaveChanges();

            //    context.ForumPosts.Add(comment);
            //    context.SaveChanges();
            //}

            //if (!context.Articles.Any())
            //{
            //    context.Articles.Add(article);
            //    context.SaveChanges();
            //}



        }
    }
}
