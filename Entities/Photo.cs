using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Entities
{
    public class Photo
    {
        [Required]
        public string PhotoId { get; set; }
        [Required]
        public string Url { get; set; }

        public string AltText { get; set; }
        public string PhotoDescription { get; set; } = null;
    }
}
