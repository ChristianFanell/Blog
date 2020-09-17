using FoodBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.DTO
{
    public class ForumPostDTO
    {
        public int ForumPostId { get; set; }

        public string AuthorName { get; set; }

        public int AuthorId { get; set; }

        public DateTime Created { get; set; }

        public string CommentContent { get; set; }

        public bool Edited { get; set; } = false;

        public int Likings { get; set; } = 0;
    }
}
