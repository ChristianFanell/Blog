using System.Threading.Tasks;
using FoodBlogApi.Entities;
using FoodBlogApi.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodBlogApi.Services {
    public class UserRepository : IUserRepository 
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository (
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
        ) {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<Author> AddUser (User newUser) 
        {
            var person = new Author {
                About = newUser.About,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                NickName = newUser.NickName,
            };
            var identityUser = new IdentityUser (newUser.Email);
            var added = _context.Authors.Add (person);

            await _userManager.CreateAsync (identityUser, newUser.Password);
            await _userManager.AddToRoleAsync (identityUser, newUser.Role);
            await _context.SaveChangesAsync ();

            return added.Entity;
        }
    }
}