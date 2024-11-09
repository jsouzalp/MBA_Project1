using AutoMapper;
using Blog.Entities.Authors;

namespace Blog.Services.AutoMapper
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorOutput>()
                .ForMember(dest => dest.Id, source => source.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, source => source.MapFrom(x => x.Name));
            //.ForMember(dest => dest.TotalPosts, opt => opt.MapFrom(src => src.Posts.Count()))
            //.ForMember(dest => dest.LastPostDate, opt => opt.MapFrom(src =>
            //    src.Posts.OrderByDescending(p => p.Date).Select(p => (DateTime?)p.Date).FirstOrDefault() == DateTime.MinValue
            //        ? (DateTime?)null
            //        : src.Posts.OrderByDescending(p => p.Date).Select(p => (DateTime?)p.Date).FirstOrDefault()))
            //.ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts));

            CreateMap<AuthorInput, Author>()
                .ForMember(dest => dest.Id, source => source.MapFrom(x => x.Id))
                .ForMember(dest => dest.IdentityUser, source => source.MapFrom(x => x.IdentityUser))
                .ForMember(dest => dest.Name, source => source.MapFrom(x => x.Name));
        }
    }
}
