using BAL.ViewModels;
using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAnswerService
    {
        IEnumerable<AnswerViewModel> GetAnswersByQId(int questionId);
        IEnumerable<AnswerViewModel> GetAnswersByUIdAndQId(long userId, long questionId);
        bool AddAnswer(AnswerViewModel answerViewModel);
    }
}
