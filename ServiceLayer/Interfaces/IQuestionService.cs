using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetQuestionsBySId(int sessionId);
        Question GetById(int questionId);
        Question Insert(Question question);
        bool Update(Question question);
    }
}
