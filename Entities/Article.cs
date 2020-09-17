using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Entities
{
    public class Article
    {

        [Key]
        public int ArticleId { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [Required]
        public string ArticleName  { get; set; }

        public Categorys Category { get; set; }

        [Required]
        public string ArticleText { get; set; }

        public DateTime Created { get; set; }

        public DateTime Edited { get; set; }

        public virtual ICollection<ArticleComment> ArticleComments { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }

        [ForeignKey("PhotoId")]
        public virtual Photo Photo { get; set; }

        public int AuthorId { get; set; }

        public string PhotoId { get; set; }

        //public virtual ICollection<ArticlePhoto> Photos { get; set; }
    }
}
