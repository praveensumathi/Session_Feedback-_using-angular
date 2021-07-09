using Dapper.Contrib.Extensions;
using System;

namespace Session_Feedback.core.Models
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string FeedbackAnswer { get; set; }
        public string AnsweredBy { get; set; }
        public DateTime AnsweredOn { get; set; }

        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}
