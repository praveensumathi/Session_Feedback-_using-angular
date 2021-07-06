using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
namespace Session_Feedback.core.Models
{
    [Table("Sessions")]
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
