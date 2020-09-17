using FoodBlogApi.DTO;
using FoodBlogApi.Entities;
using FoodBlogApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Services
{
    public interface IForumPostRepository
    {




        // ForumPosts crud
        Task<ForumPostDTO> GetForumPostAsync(int id);

        //Task<List<ForumPostDTO>> GetForumPostsAsync();
        // overload
        Task<List<ForumPostDTO>> GetForumPostsAsync(Pagination pages);

        Task<ForumPost> AddForumPostAsync(ForumPost ForumPost);

        Task<ForumPost> DeleteForumPostAsync(int id);

        Task<ForumPost> GetForumPostForPatch(int id);

        Task<ForumPost> DownVoteForumPost(int id);

        Task<ForumPost> UpVoteForumPost(int id);

        //Task<ForumPostDTO> UpdateForumPost(int id, JsonPatchDocument<ForumPost> patchDoc);


        // author info
        Task<AuthorDTO> GetAuthorAsync(string email);
    }
}
