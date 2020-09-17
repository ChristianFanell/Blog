using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodBlogApi.Controllers
{
    public class UserController : ControllerBase
    {        
        // todo: userdto med statistik
        [HttpGet]
        public IActionResult GetUserInformation(int id) 
        {
            return Ok("User info");
        }

        // username, about 
        [HttpPatch]
        public IActionResult UpdateUserInformation(int id) 
        {
            return Ok("Update user");
        }

        [HttpPost]
        public IActionResult UploadPicture(IFormFile photo) 
        {
            return Ok("photo uploaded");
        }



    }
}