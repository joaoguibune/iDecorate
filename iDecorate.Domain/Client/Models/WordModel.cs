using System;

namespace iDecorate.Domain.Client.Models
{
    public class WordModel
    {
        public Guid id { get; set; }
        public Guid id_product { get; set; }
        public string description { get; set; }
        public string meaning { get; set; }
        public TopicModel topic { get; set; }
    }
}
