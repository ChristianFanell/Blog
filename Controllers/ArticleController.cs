using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using FoodBlogApi.Entities;
using FoodBlogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly IArticleRepository repo;
        private readonly IPhotoRepository photoRepo;

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
                if (article.Image != null)
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


        [HttpGet("id/{id}", Name = nameof(GetArticle))]
        public async Task<IActionResult>GetArticle(int id)
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

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            try
            {
                var articles = await repo.GetArticlesAsync();

                return Ok(articles);
            }
            catch (Exception err)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Could not retrieve articles: {err.Message}");
            }
        }

        [HttpGet("{name}")]
        public IActionResult GetCategorys()
        {
            try
            {
                var categoryValues = Enum.GetValues(typeof(Categorys)).Cast<Categorys>().ToList();
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
