using AutoMapper;
using Blog.Entities.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.AutoMapper.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorOutput>()
                .ForMember(dest => dest.Id, source => source.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, source => source.MapFrom(x => x.Name))
                .ForMember(dest => dest.TotalPosts, opt => opt.MapFrom(src => src.Posts.Count()))
                .ForMember(dest => dest.LastPostDate, opt => opt.MapFrom(src => src.Posts.OrderByDescending(p => p.Date).Select(p => p.Date).FirstOrDefault()))
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts));

            CreateMap<AuthorInput, Author>()
                .ForMember(dest => dest.Id, source => source.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, source => source.MapFrom(x => x.Name));
        }
    }
}
