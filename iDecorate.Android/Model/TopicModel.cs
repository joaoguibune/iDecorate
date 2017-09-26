using System;
using System.Collections.Generic;

namespace iDecorate.Android.Model
{
    public class TopicModel
    {
        public Guid id { get; set; }
        public string description { get; set; }
        public List<Word> words { get; set; }
    }
}
