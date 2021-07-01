using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Session_Feedback.core.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public ICollection<Question> Questions { get; set; }

    }
}
