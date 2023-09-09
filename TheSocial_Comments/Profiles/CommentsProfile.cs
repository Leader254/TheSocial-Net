using AutoMapper;
using TheSocial_Comments.Models;
using TheSocial_Comments.Models.Dtos;

namespace TheSocial_Comments.Profiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            // Source -> Target
            CreateMap<CommentRequestDto, Comment>().ReverseMap();
        }
    }
}
