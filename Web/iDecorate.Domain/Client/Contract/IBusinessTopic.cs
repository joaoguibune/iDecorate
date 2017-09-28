using iDecorate.Domain.Client.Models;
using System;
using System.Collections.Generic;

namespace iDecorate.Domain.Client.Contract
{
    public interface IBusinessTopic
    {
        bool Insert(TopicModel reminderViewModel);
        bool Update(TopicModel reminderViewModel);
        bool Delete(Guid key);
        List<TopicModel> GetAll();
        TopicModel Find(Guid key);
    }
}
