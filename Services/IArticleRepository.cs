using FoodBlogApi.DTO;
using FoodBlogApi.Entities;
using FoodBlogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Services
{
    public interface IArticleRepository
    {
        Task<List<ArticleDTO>> GetArticlesAsync(Pagination pages);
        Task<ArticleDTO> GetArticleAsync(int id);
        Task<Article> AddArticle(Article article);
        Task<Article> DeleteArticleAsync(Article article);
        Task<Article> GetArticleToDeleteAsync(int articleId);
    }
}
