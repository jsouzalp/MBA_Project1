using AutoMapper;
using Blog.Entities.Posts;

namespace Blog.AutoMapper.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostOutput>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, source => source.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.Date, source => source.MapFrom(src => src.Date))
                .ForMember(dest => dest.Title, source => source.MapFrom(src => src.Title))
                .ForMember(dest => dest.Message, source => source.MapFrom(src => src.Message))
                //.ForMember(dest => dest.TotalComments, source => source.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.Comments, source => source.MapFrom(src => src.Comments));

            CreateMap<PostInput, Post>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, source => source.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.Title, source => source.MapFrom(src => src.Title))
                .ForMember(dest => dest.Message, source => source.MapFrom(src => src.Message));
        }
    }
}
