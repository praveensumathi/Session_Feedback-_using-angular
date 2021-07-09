using System;

namespace API.Models
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string FeedbackQuestion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public int SessionId { get; set; }
        //public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
    }
}
