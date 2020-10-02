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

        public async Task<Photo> DeletePhoto(string id)
        {
            var photoToDeleteInDb = await _context.Photos.FindAsync(id);

            if (photoToDeleteInDb is null)
            {
                throw new Exception("could not find photo in db");
            }
            var photoToDeletedInCloud = _photoAccessor.DeletePhoto(id);

            if (photoToDeletedInCloud is null)
            {
                throw new Exception("Could not delete the photoo");
            }
            
            var deletedPhoto = _context.Photos.Remove(photoToDeleteInDb);
 
            await _context.SaveChangesAsync();
            return deletedPhoto.Entity;
        }
    }
}
