using System;

namespace iDecorate.Domain.Client.Models
{
    public class WordModel
    {
        public Guid id { get; set; }
        public Guid topic_id { get; set; }
        public string description { get; set; }
        public string meaning { get; set; }
    }
}
