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
            CreateMap<Author, AuthorRequestTO>();
            CreateMap<Author, AuthorResponseTO>();
            CreateMap<Marker, MarkerRequestTO>();
            CreateMap<Marker, MarkerResponseTO>();
            CreateMap<Post, PostRequestTO>();
            CreateMap<Post, PostResponseTO>();
            CreateMap<Tweet,  TweetRequestTO>();
            CreateMap<Tweet, TweetResponseTO>();
        }
    }
}
