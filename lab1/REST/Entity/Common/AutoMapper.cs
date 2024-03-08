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
            CreateMap<AuthorRequestTO, Author>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => 1));
            CreateMap<Author, AuthorResponseTO>();

            CreateMap<MarkerRequestTO, Marker>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => 1));
            CreateMap<Marker, MarkerResponseTO>();

            CreateMap<PostRequestTO, Post>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => 1));
            CreateMap<Post, PostResponseTO>();

            CreateMap<TweetRequestTO, Tweet>()
                .ForMember(dst => dst.Author, map => map.MapFrom(src => new Author() { Id = src.AuthorId }))
                .ForMember(dst => dst.Id, map => map.MapFrom(src => 1));
            CreateMap<Tweet, TweetResponseTO>()
                .ForMember(dst => dst.AuthorId, map => map.MapFrom(src => src.Author.Id));
        }
    }
}
