using AutoMapper;
using iDecorate.Data.Entity;
using iDecorate.Domain.Client.Models;

namespace iDecorate.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<TopicModel, TopicEntity>();
            CreateMap<TopicEntity, TopicModel>();
        }
    }
}