using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodBlogApi.Entities;
using FoodBlogApi.Models;
using FoodBlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodBlogApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly IArticleRepository repo;
        private readonly IPhotoRepository photoRepo;
        private const int fileSize = 1048576;

        public ArticleController(IArticleRepository repo, IPhotoRepository photoReop)
        {
            this.repo = repo;
            this.photoRepo = photoReop;

        }

        [HttpPost]
        public async Task<IActionResult> AddArticle([FromForm] Article article)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (article.Image != null && article.Image.ContentType.Contains("image") && article.Image.Length < fileSize)
                {
                    var image = await photoRepo.AddPhoto(article.Image);

                    article.PhotoId = image.PhotoId;
                }

                await repo.AddArticle(article);
                return new CreatedAtRouteResult(nameof(GetArticle), new { id = article.AuthorId }, article);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
            }
        }

        [AllowAnonymous] // for dev only
        [HttpDelete("{articleId:int}")]
        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            try
            {
                var article = await repo.GetArticleToDeleteAsync(articleId);

                if (article is null)
                {
                    return NotFound("No article was found");
                }
                if (article.PhotoId != null)
                {
                    await photoRepo.DeletePhoto(article.PhotoId);
                }
                var result = await repo.DeleteArticleAsync(article);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
            }
           
        }

        [AllowAnonymous]
        [HttpGet("id/{id}", Name = nameof(GetArticle))]
        public async Task<IActionResult> GetArticle(int id)
        {
            try
            {
                var article = await repo.GetArticleAsync(id);

                if (article is null)
                {
                    return BadRequest($"Could not find article with id {id}");
                }

                return Ok(article);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Could not retrieve articles: {e.Message}");

            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            try
            {
                var pages = new Pagination { NumberOfPosts = 5, Offset = 0 };
                var articles = await repo.GetArticlesAsync(pages);

                return Ok(articles);
            }
            catch (Exception err)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Could not retrieve articles: {err.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("{name}")]
        public IActionResult GetCategorys()
        {
            try
            {
                var categoryValues = Enum.GetValues(typeof(Entities.Categorys)).Cast<Entities.Categorys>().ToList();
                var strings = new List<string>();

                foreach (var item in categoryValues)
                {
                    strings.Add(item.ToString());
                }
                return Ok(strings);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retriving categorys: {e.Message}");
            }
        }






    }
}
