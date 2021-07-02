using Dapper.Contrib.Extensions;
using System;

namespace Session_Feedback.core.Models
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public string QuestionAnswer { get; set; }
        public string AnsweredBy { get; set; }
        public DateTime AnsweredOn { get; set; }
    }
}
