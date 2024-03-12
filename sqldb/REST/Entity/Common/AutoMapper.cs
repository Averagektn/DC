using AutoMapper;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;

namespace REST.Entity.Common
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<AuthorRequestTO, Author>();
            CreateMap<Author, AuthorResponseTO>();

            CreateMap<MarkerRequestTO, Marker>();
            CreateMap<Marker, MarkerResponseTO>();

            CreateMap<PostRequestTO, Post>();
            CreateMap<Post, PostResponseTO>();

            CreateMap<TweetRequestTO, Tweet>()
                .ForMember(dst => dst.Author, map => map.MapFrom(src => new Author() { Id = src.AuthorId }));
            CreateMap<Tweet, TweetResponseTO>()
                .ForMember(dst => dst.AuthorId, map => map.MapFrom(src => src.Author.Id));
        }
    }
}
