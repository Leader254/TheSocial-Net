using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TheSocial_Post.Models;
using TheSocial_Post.Models.Dtos;

namespace TheSocial_Post.Profiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<PostRequestDto, Post>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
        }
    }
}