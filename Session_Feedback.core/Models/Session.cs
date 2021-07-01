using System;
using System.ComponentModel.DataAnnotations;
namespace Session_Feedback.core.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int FeedbackId { get; set; }
        public Feedback Feedback { get; set; }
    }
}
