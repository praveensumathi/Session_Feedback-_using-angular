using BAL.ViewModels;
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
        IEnumerable<QuestionViewModel> GetQuestionsBySId(int sessionId);
        QuestionViewModel GetById(int questionId);
        QuestionViewModel Insert(QuestionViewModel question);
        bool Update(QuestionViewModel question);
    }
}
