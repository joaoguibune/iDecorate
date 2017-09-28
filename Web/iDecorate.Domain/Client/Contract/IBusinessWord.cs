using iDecorate.Domain.Client.Models;
using System;
using System.Collections.Generic;

namespace iDecorate.Domain.Client.Contract
{
    public interface IBusinessWord
    {
        bool Insert(WordModel wordModel);
        bool Update(WordModel wordModel);
        bool Delete(Guid key);
        List<WordModel> GetAll();
        WordModel Find(Guid key);
    }
}
