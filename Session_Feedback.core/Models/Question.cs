using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Session_Feedback.core.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string FeedbackQuestion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
