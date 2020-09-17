using AutoMapper;
using FoodBlogApi.DTO;
using FoodBlogApi.Entities;
using FoodBlogApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Services
{
    public class EFArticleRepository : IArticleRepository
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EFArticleRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Article> AddArticle(Article article)
        {
            var addedArticle = await context.Articles.AddAsync(article);
            
            await context.SaveChangesAsync();
            return addedArticle.Entity;
        }

        public async Task<ArticleDTO> GetArticleAsync(int id)
        {
            var article = await context.Articles
                .Include(td => td.Photo)
                .FirstOrDefaultAsync(td => td.ArticleId == id);
            var articleDTO = mapper.Map<ArticleDTO>(article);

            return articleDTO;
        }

        // articles crud
        public async Task<List<ArticleDTO>> GetArticlesAsync()
        {
            var articles = await context.Articles.AsNoTracking()
                .Include(td => td.Author)
                .Include(td => td.Photo)
                .OrderByDescending(td => td.ArticleId)
                .ToListAsync();
                
            var articleDTO = mapper.Map<List<ArticleDTO>>(articles);

            return articleDTO;
        }

        

    }
}
