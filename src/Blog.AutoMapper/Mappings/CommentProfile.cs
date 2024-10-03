using AutoMapper;
using Blog.Entities.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.AutoMapper.Mappings
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
                .ForMember(dest => dest.AuthorName, source => source.MapFrom(src => src.CommentAuthor.Name))
                .ForMember(dest => dest.Message, source => source.MapFrom(src => src.Message));
        }
    }
}
