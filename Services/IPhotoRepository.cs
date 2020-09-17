using FoodBlogApi.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FoodBlogApi.Services
{
    public interface IPhotoRepository
    {
        Task<Photo> AddPhoto(IFormFile file);

        Task<Photo> DeletePhoto(string id);
    }
}
