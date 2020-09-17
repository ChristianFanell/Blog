using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Entities
{
    public class ArticleComment
    {
        public int ArticleCommentId { get; set; }

        public string CommentContent { get; set; }

        public Author Author { get; set; }

        public DateTime Created { get; set; }

        public DateTime Edited { get; set; }
             
    }
}
