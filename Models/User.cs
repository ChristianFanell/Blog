using FoodBlogApi.Entities;

namespace FoodBlogApi.Models
{
    public class User : Author
    {
        public string Password {get; set;}

        public string Role {get; set;}
    }
}