using System;
using System.ComponentModel.DataAnnotations;

namespace Session_Feedback.core.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public string QuestionAnswer { get; set; }
        public string AnsweredBy { get; set; }
        public DateTime AnsweredOn { get; set; }
    }
}
