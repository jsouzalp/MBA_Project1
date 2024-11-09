using AutoMapper;
using Blog.Entities.Comments;

namespace Blog.Services.AutoMapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentOutput>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
                .ForMember(dest => dest.PostId, source => source.MapFrom(src => src.PostId))
                .ForMember(dest => dest.CommentAuthorId, source => source.MapFrom(src => src.CommentAuthorId))
                .ForMember(dest => dest.Date, source => source.MapFrom(src => src.Date))
                .ForMember(dest => dest.CommentAuthorName, source => source.MapFrom(src => src.CommentAuthorName))
                .ForMember(dest => dest.Message, source => source.MapFrom(src => src.Message));

            CreateMap<CommentInput, Comment>()
                .ForMember(dest => dest.Id, source => source.MapFrom(x => x.Id))
                .ForMember(dest => dest.PostId, source => source.MapFrom(x => x.PostId))
                .ForMember(dest => dest.CommentAuthorId, source => source.MapFrom(x => x.CommentAuthorId))
                .ForMember(dest => dest.Message, source => source.MapFrom(x => x.Message));

        }
    }
}
