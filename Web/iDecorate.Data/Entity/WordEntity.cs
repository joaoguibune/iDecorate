using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iDecorate.Data.Entity
{
    public class WordEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid id { get; set; }
        public string description { get; set; }
        public string meaning { get; set; }
        public TopicEntity topic { get; set; }
    }
}