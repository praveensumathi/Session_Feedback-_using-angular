using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string FeedbackAnswer { get; set; }
        public string AnsweredBy { get; set; }
        public DateTime AnsweredOn { get; set; }

        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}
