using AutoMapper;
using iDecorate.Data.Entity;
using iDecorate.Domain.Client.Contract;
using iDecorate.Domain.Client.Models;
using iDecorate.Domain.Server.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iDecorate.Domain.Client.Business
{
    public class BusinessTopic : IBusinessTopic
    {
        private readonly IRepository<TopicEntity> _repository;
        private readonly IMapper _mapper;

        public BusinessTopic(IRepository<TopicEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public bool Insert(TopicModel topicModel)
        {
            try
            {
                var topic = _mapper.Map<TopicModel, TopicEntity>(topicModel);

                _repository.Insert(topic);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(TopicModel topicModel)
        {
            try
            {
                var topic = _mapper.Map<TopicModel, TopicEntity>(topicModel);
                
                _repository.Update(topic);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int key)
        {
            try
            {
                var reminder = _repository.Find(key);

                _repository.Delete(reminder);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TopicModel Find(int key)
        {
            try
            {
                var topic = _repository.Find(key);

                var result = _mapper.Map<TopicEntity, TopicModel>(topic);
                                
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TopicModel> GetAll()
        {
            try
            {
                var topics = _repository.GetAll().OrderBy(r => r.description).ToList();

                var result = _mapper.Map<List<TopicEntity>, List<TopicModel>>(topics);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}