using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ConductedBy { get; set; }
        public DateTime? ConductedOn { get; set; }

        // public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();
    }
}
