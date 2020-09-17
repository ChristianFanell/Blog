using AutoMapper;
using FoodBlogApi.DTO;
using FoodBlogApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.AutoMapperHelpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            // converts enum Categorys to string value
            CreateMap<Categorys, string>().ConvertUsing(src => src.ToString());

            // article
            CreateMap<Article, ArticleDTO>()
                .ForMember(
                    dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author.NickName)
                )
                .ForMember(
                    dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Photo.Url)
                )
                .ForMember(
                    dest => dest.ImageId,
                    opt => opt.MapFrom(src => src.Photo.PhotoId)
                );
                

            // forum
            CreateMap<ForumPost, ForumPostDTO>()
                .ForMember(
                    dest => dest.AuthorName, 
                    opt => opt.MapFrom(src => src.Author.NickName)
                );

            // author
            CreateMap<Author, AuthorDTO>();


        }

    }
}
