using FoodBlogApi.DTO;
using FoodBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Services
{
    public interface IArticleRepository
    {
        Task<List<ArticleDTO>> GetArticlesAsync();
        Task<ArticleDTO> GetArticleAsync(int id);
        Task<Article> AddArticle(Article article);
    }
}
