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
        private readonly IMapper _mapper;

        public BusinessWord(IRepository<WordEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public bool Insert(WordModel wordModel)
        {
            try
            {
                var word = _mapper.Map<WordModel, WordEntity>(wordModel);

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
                
                _repository.Update(word);

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

        public WordModel Find(int key)
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