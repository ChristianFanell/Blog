using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Models
{
    public class Pagination
    {
        public int? NumberOfPosts { get; set; }

        public int? Offset { get; set; }
    }
}
