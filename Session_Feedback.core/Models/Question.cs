﻿using Dapper.Contrib.Extensions;
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
        public ICollection<Answer> Answers { get; set; }
    }
}
