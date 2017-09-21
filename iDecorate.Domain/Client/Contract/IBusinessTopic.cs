using iDecorate.Domain.Client.Models;
using System.Collections.Generic;

namespace iDecorate.Domain.Client.Contract
{
    public interface IBusinessTopic
    {
        bool Insert(TopicModel reminderViewModel);
        bool Update(TopicModel reminderViewModel);
        bool Delete(int key);
        List<TopicModel> GetAll();
        TopicModel Find(int key);
    }
}
