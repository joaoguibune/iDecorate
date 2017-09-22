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
    public class BusinessWord : IBusinessWord
    {
        private readonly IRepository<WordEntity> _repository;
        private readonly IRepository<TopicEntity> _repositoryTopic;
        private readonly IMapper _mapper;

        public BusinessWord(IRepository<WordEntity> repositoryWord, IRepository<TopicEntity> repositoryTopic, IMapper mapper)
        {
            _repository = repositoryWord;
            _repositoryTopic = repositoryTopic;
            _mapper = mapper;
        }

        public bool Insert(WordModel wordModel)
        {
            try
            {
                var word = _mapper.Map<WordModel, WordEntity>(wordModel);

                var topic = _repositoryTopic.Find(wordModel.topic_id);

                word.topic = topic;

                _repository.Insert(word);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(WordModel wordModel)
        {
            try
            {
                var word = _mapper.Map<WordModel, WordEntity>(wordModel);

                var topic = _repositoryTopic.Find(wordModel.topic_id);

                word.topic = topic;

                _repository.Update(word);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Guid key)
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

        public WordModel Find(Guid key)
        {
            try
            {
                var word = _repository.Find(key);

                var result = _mapper.Map<WordEntity, WordModel>(word);
                                
                return result;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WordModel> GetAll()
        {
            try
            {
                var words = _repository.GetAll().OrderBy(r => r.description).ToList();

                var result = _mapper.Map<List<WordEntity>, List<WordModel>>(words);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}