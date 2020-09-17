using FoodBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.DTO
{
    public class ArticleDTO
    {
        public int ArticleId { get; set; }

        public string ImageId { get; set; }

        public string ImageUrl { get; set; }

        public string ArticleName { get; set; }

        public string Category { get; set; }

        public string ArticleText { get; set; }
 
        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        public DateTime Edited { get; set; }
    }
}
