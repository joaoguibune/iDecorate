using iDecorate.Domain.Client.Models;
using System.Collections.Generic;

namespace iDecorate.Domain.Client.Contract
{
    public interface IBusinessWord
    {
        bool Insert(WordModel reminderViewModel);
        bool Update(WordModel reminderViewModel);
        bool Delete(int key);
        List<WordModel> GetAll();
        WordModel Find(int key);
    }
}
