using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AnswerDTO
    {
        public int AnswerId { get; set; }
        public string QuestionAnswer { get; set; }
        public string AnsweredBy { get; set; }
        public DateTime AnsweredOn { get; set; }

        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}
