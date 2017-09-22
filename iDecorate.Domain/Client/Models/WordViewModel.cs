using System;

namespace iDecorate.Domain.Client.Models
{
    public class WordExpressionsModel
    {
        public Guid word_id { get; set; }
        public Guid topic_id { get; set; }
        public string word_description { get; set; }
        public string topic_description { get; set; }
        public string word_meaning { get; set; }

        public WordModel toWordModel()
        {
            return new WordModel() { id = word_id, description = word_description, meaning = word_meaning, topic_id = topic_id };
        }
    }
}
