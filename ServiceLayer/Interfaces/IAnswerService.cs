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
        IEnumerable<Answer> GetAnswersByQId(int questionId);
        IEnumerable<Answer> GetAnswersByUIdAndQId(long userId, long questionId);
    }
}
