using Dapper;
using ServiceLayer.Interfaces;
using Session_Feedback.core.Models;
using Session_Feedback.core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ModelServices
{
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string StoreProcedure = "Answer";

        public AnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Answer> GetAnswersByQId(int questionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@QuestionId", questionId);
            parms.Add("@StatementType", "SelectByQId");

            var answers = _unitOfWork.Answers.GetAnswersByQId(StoreProcedure, parms);
            _unitOfWork.Commit();

            return answers;
        }

        public IEnumerable<Answer> GetAnswersByUIdAndQId(long userId, long questionId)
        {
            throw new NotImplementedException();
        }
    }
}
