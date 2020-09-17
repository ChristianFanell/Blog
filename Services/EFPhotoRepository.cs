using FoodBlogApi.Entities;
using FoodBlogApi.Models;
using FoodBlogApi.PhotoManagement;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Services
{
    public class EFPhotoRepository : IPhotoRepository
    {
        private readonly IPhotoAccessor _photoAccessor;
        private readonly ApplicationDbContext _context;

        public EFPhotoRepository(IPhotoAccessor photoAccessor, ApplicationDbContext context)
        {
            _photoAccessor = photoAccessor;
            _context = context;

        }
        public async Task<Photo> AddPhoto(IFormFile file)
        {
            var photoUploadResult = _photoAccessor.AddPhoto(file);
            var photo = new Photo
            {
                PhotoId = photoUploadResult.PublicId,
                Url = photoUploadResult.Url
            };
            var added = _context.Photos.Add(photo);
            
            await _context.SaveChangesAsync();
            return added.Entity;
        }

        public Task<Photo> DeletePhoto(string id)
        {
            throw new NotImplementedException();
        }
    }
}
