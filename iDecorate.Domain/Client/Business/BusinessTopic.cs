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
        private readonly IRepository<TopicEntity> _repositoryTopic;
        private readonly IRepository<WordEntity> _repositoryWord;
        private readonly IMapper _mapper;

        public BusinessTopic(IRepository<TopicEntity> repositoryTopic, IRepository<WordEntity> repositoryWord, IMapper mapper)
        {
            _repositoryTopic = repositoryTopic;
            _repositoryWord = repositoryWord;
            _mapper = mapper;
        }

        public bool Insert(TopicModel topicModel)
        {
            try
            {
                var topic = _mapper.Map<TopicModel, TopicEntity>(topicModel);

                _repositoryTopic.Insert(topic);

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
                
                _repositoryTopic.Update(topic);

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
                var reminder = _repositoryTopic.Find(key);

                _repositoryTopic.Delete(reminder);

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
                var topic = _repositoryTopic.Find(key);

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
                var topics = _repositoryTopic.GetAll().OrderBy(r => r.description).ToList();

                topics.ForEach(t => {
                    var words = _repositoryWord.GetAll().Where(w => w.topic.id.Equals(t.id)).ToList();
                    t.words = words;
                });

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