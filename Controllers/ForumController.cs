using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodBlogApi.DTO;
using FoodBlogApi.Entities;
using FoodBlogApi.Models;
using FoodBlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FoodBlogApi.Controllers
{
    [Authorize(Roles = "User, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumPostRepository repo;
        private readonly ApplicationDbContext context;

        public ForumController(IForumPostRepository repo, ApplicationDbContext context)
        {
            this.repo = repo;
            this.context = context;
        }



        // https://localhost:44371/api/forum?offset=3&numberofposts=1
        [HttpGet]
        public async Task<IActionResult> GetForumPosts([FromQuery] Pagination pages)
        {
            try
            {
                var forumPosts = await repo.GetForumPostsAsync(pages);

                return Ok(forumPosts);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Could not retrieve comments: {err.Message}");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetForumPost(int id)
        {
            try
            {
                var comment = await repo.GetForumPostAsync(id);

                if (comment is null)
                {
                    return NotFound();
                }
                return Ok(comment);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error finding comment with id {id}: {err.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddForumPost([FromBody] ForumPost comment)
        {
            try
            {
                if (comment is null)
                {
                    return BadRequest();
                }
                var forumPost = await repo.AddForumPostAsync(comment);

                return CreatedAtAction(nameof(GetForumPost), new { id = forumPost.ForumPostId}, forumPost);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding to DB: {err.Message}");
            }
        }

        //        [
        //    {
        //        "op": "replace",
        //        "path": "/commentContent",
        //        "value": "Redigerat inlägg"
        //    },
        //    {
        //        "op": "replace",
        //        "path": "/edited",
        //        "value": true
        //    }
        //]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateForumPostWithPatch(int id, [FromBody]JsonPatchDocument<ForumPost> patchDoc)
        {
            try
            {
                if (patchDoc is null) return BadRequest();

                var forumPostToUpdate = await repo.GetForumPostForPatch(id);

                if (forumPostToUpdate == null) return NotFound();

                // requires newtonsoftjson
                patchDoc.ApplyTo(forumPostToUpdate, ModelState);

                var isValid = TryValidateModel(forumPostToUpdate);

                if (!isValid) return BadRequest(ModelState);

                await context.SaveChangesAsync();

                return Ok("Uppdaterat");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Could not uppdate comment: {ex.Message}"); ;
            }

            

        }


    }
}
