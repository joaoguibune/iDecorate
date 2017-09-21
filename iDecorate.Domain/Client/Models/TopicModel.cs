using System;
using System.Collections.Generic;

namespace iDecorate.Domain.Client.Models
{
    public class TopicModel
    {
        public Guid id { get; set; }
        public string description { get; set; }
        public List<WordModel> words { get; set; }
    }
}
