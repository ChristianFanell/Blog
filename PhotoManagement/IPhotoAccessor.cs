using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FoodBlogApi.PhotoManagement
{
    public interface IPhotoAccessor
    {
        PhotoUploadResult AddPhoto(IFormFile file);
        string DeletePhoto(string imageId);

    }
}
