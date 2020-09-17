using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Entities
{
    public class ForumPost
    {
        public int ForumPostId { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 2)]
        public string CommentContent { get; set; }

        public int Likings { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public bool Edited { get; set; }
    }
}
