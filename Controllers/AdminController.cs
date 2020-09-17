using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodBlogApi.Models;
using FoodBlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodBlogApi.Controllers
{
    // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IForumPostRepository _forumRepo;
        private readonly IUserRepository _userRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            IForumPostRepository forumRepo,
            IUserRepository userRepository,
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager
            )
            
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _forumRepo = forumRepo;
            _userRepo = userRepository;

        }


        // TODO:
        [HttpDelete]
        public IActionResult DeleteForumPost(int id)
        {
            return Ok("Deleted");
        }

        [HttpPut] 
        public IActionResult UpdateArticle(int id)
        {
            return Ok("updated");
        }

        [HttpPatch]
        public IActionResult BanUserFromForum() 
        {
            return Ok("User is now banned!");
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] User newUser)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByNameAsync(newUser.Email) != null)
                {
                    return BadRequest("Email is already taken");
                }
                var addedUser = await _userRepo.AddUser(newUser);

                return Ok(addedUser);
            }

            return BadRequest("Could not add user");

        }

    }
}
