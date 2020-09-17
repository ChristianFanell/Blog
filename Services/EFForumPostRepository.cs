using AutoMapper;
using FoodBlogApi.DTO;
using FoodBlogApi.Entities;
using FoodBlogApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Models
{
    public class EFForumPostRepository : IForumPostRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EFForumPostRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
               

        public async Task<AuthorDTO> GetAuthorAsync(string email)
        {
            var user = await Task.Run(() => context.Authors.Where(td => td.Email == email).FirstOrDefault());
            var userDTO = mapper.Map<AuthorDTO>(user);

            return userDTO;
        }

        // ForumPosts crud

        public async Task<List<ForumPostDTO>> GetForumPostsAsync(Pagination pages)
        {
            var querable = context.ForumPosts.Include(td => td.Author).AsQueryable();
            var ForumPosts = await querable
                .Skip(pages.Offset ?? 0)
                .Take(pages.NumberOfPosts ?? 20)
                .OrderByDescending(td => td.Created)
                .ToListAsync();
            var ForumPostDTO = mapper.Map<List<ForumPostDTO>>(ForumPosts);

            return ForumPostDTO;
        }


        // httppost
        public async Task<ForumPost> AddForumPostAsync(ForumPost ForumPost)
        {
            var result = await context.ForumPosts.AddAsync(ForumPost);

            ForumPost.Created = DateTime.Now;

            await context.SaveChangesAsync();
            return result.Entity;
        }

        // httpget
        public async Task<ForumPostDTO> GetForumPostAsync(int id)
        {
            var forumPost = await context.ForumPosts
                .Include(td => td.Author)
                .FirstOrDefaultAsync(td => td.ForumPostId == id);
            var forumPostDTO = mapper.Map<ForumPostDTO>(forumPost);

            return forumPostDTO;
        }

   
        public async Task<ForumPost> GetForumPostForPatch(int id)
        {
            var forumPost = await context.ForumPosts
                .Include(td => td.Author)
                .FirstOrDefaultAsync(td => td.ForumPostId == id);

            return forumPost;
        }

        // httpdelete
        public async Task<ForumPost> DeleteForumPostAsync(int id)
        {
            var forumPostToDelete = await context.ForumPosts.FirstAsync(td => td.AuthorId == id);                                                                
            var result = context.ForumPosts.Remove(forumPostToDelete);

            if (result != null) await context.SaveChangesAsync();
            return result.Entity;
        }



        public Task<ForumPost> DownVoteForumPost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ForumPost> UpVoteForumPost(int id)
        {
            throw new NotImplementedException();
        }

        //// httppatch
        //public async Task<ForumPostDTO> UpdateForumPost(int id, JsonPatchDocument<ForumPost> patchDoc)
        //{
        //    var forumPostToUpdate = await context.ForumPosts.FirstOrDefaultAsync(td => td.AuthorId == id);

        //    patchDoc.ApplyTo(forumPostToUpdate, ModelState);

        //}
    }
}
