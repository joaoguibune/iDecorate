using AutoMapper;
using iDecorate.Data.Entity;
using iDecorate.Domain.Client.Models;
using System.Collections.Generic;

namespace iDecorate.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<TopicModel, TopicEntity>().MaxDepth(1);
            CreateMap<TopicEntity, TopicModel>().MaxDepth(1);

            CreateMap<WordModel, WordEntity>().MaxDepth(1);
            CreateMap<WordEntity, WordModel>().MaxDepth(1);
        }
    }
}