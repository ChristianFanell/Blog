using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Entities
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
