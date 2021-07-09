using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Session_Feedback.core.Models
{
    [Table("Question")]
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Feedback { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public int SessionId { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
