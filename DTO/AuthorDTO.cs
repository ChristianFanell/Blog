using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.DTO
{
    public class AuthorDTO
    {
        public int AutorId { get; set; }

        public int Upvotes { get; set; }

        public int NumberOfForumPosts { get; set; }

        public string NickName { get; set; }

        //public bool IsBanned { get; set; }
    }
}
