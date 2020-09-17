using FoodBlogApi.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Services
{
    public interface IAuthService
    {
        string CreateToken(LoginModel login, IList<string> role, string nickName);
    }
}
