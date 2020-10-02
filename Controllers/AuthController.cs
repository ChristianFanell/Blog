using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodBlogApi.Entities;
using FoodBlogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoodBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityUser> _roleManager;
        private IConfiguration _config;
        private IAuthService _authService;
        private IForumPostRepository _forumRepo;

        public AuthController(
            IConfiguration conf,
            UserManager<IdentityUser> usrmgr,
            SignInManager<IdentityUser> signInMgr,
            IAuthService authService,
            IForumPostRepository forumRepo            
            )
        {
            _userManager = usrmgr;
            _signInManager = signInMgr;
            _config = conf;
            _authService = authService;
            _forumRepo = forumRepo;

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Auth hälsar!");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(login.Name);

                if (user != null)
                {
                    var author = await _forumRepo.GetAuthorAsync(user.UserName);

                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(login.Name, login.Password, false, false)).Succeeded)
                    {
                        var userId = user.Id;
                        var roles = await _userManager.GetRolesAsync(user);
                        var token = _authService.CreateToken(login, roles, author.NickName);

                        return Ok(new
                        {
                            Token = token,
                            UserName = user.UserName,
                            nickName = author.NickName
                        });
                    }
                } 
            }
            return Unauthorized(new { Message = "Fel email eller lösenord" });

        }


    }
}
