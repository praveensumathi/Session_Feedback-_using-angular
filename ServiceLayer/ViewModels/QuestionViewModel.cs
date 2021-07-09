using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public int SessionId { get; set; }

        //public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
    }
}
