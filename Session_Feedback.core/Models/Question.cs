using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Session_Feedback.core.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
     
        public string FeedbackQuestion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int SessionId { get; set; }

        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
