using System.Threading.Tasks;
using FoodBlogApi.Entities;
using FoodBlogApi.Models;

namespace FoodBlogApi.Services {
    public interface IUserRepository 
    {
        Task<Author> AddUser (User newUser);

    }
}