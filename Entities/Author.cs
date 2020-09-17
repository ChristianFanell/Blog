using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Entities
{
    public class Author
    {

        public int AuthorId { get; set; }
        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string NickName { get; set; }

        [ForeignKey("PhotoId")]
        public virtual Photo Photo { get; set; }

        public string PhotoId { get; set; }

        public string About { get; set; }

        //public bool IsBanned { get; set; } = false;

        //public int Likings { get; set; }

    }
}
