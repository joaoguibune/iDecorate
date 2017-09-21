using System;

namespace iDecorate.Data.Entity
{
    public class WordEntity
    {
        public Guid id { get; set; }
        public Guid id_product { get; set; }
        public string description { get; set; }
        public string meaning { get; set; }
        public TopicEntity topic { get; set; }
    }
}