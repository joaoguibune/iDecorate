using System;

namespace iDecorate.Android.Domain.Model
{
    public class TopicWordModel
    {
        public Guid topic_id;
        public Guid word_id;

        public string topic_description { get; set; }
        public string word_description { get; set; }
        public string word_meaning { get; set; }
    }
}
