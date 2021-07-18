using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
namespace Session_Feedback.core.Models
{
    [Table("Session")]
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ConductedBy { get; set; }
        public DateTime ConductedOn { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
