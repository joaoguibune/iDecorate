using System;
using System.Collections.Generic;

namespace iDecorate.Data.Entity
{
    public class TopicEntity
    {
        public Guid id { get; set; }
        public Guid id_user { get; set; }
        public string description { get; set; }
        public List<WordEntity> words { get; set; }
    }
}