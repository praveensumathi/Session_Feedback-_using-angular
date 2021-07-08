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
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string StoreProcedure = "Question";

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Question> GetQuestionsBySId(int sessionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectBySId");
            parms.Add("@SessionId", sessionId);

            var questions = _unitOfWork.Questions.GetQuestionsBySId(StoreProcedure, parms);
            _unitOfWork.Commit();
            return questions;
        }

        public Question Insert(Question question)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "Insert");
            parms.Add("@FeedbackQuestion", question.FeedbackQuestion);
            parms.Add("@CreatedBy", question.CreatedBy);
            parms.Add("@CreatedOn", DateTime.Now);
            parms.Add("@SessionId", question.SessionId);

            var insertedId = _unitOfWork.Questions.Insert(StoreProcedure, parms);
            _unitOfWork.Commit();
            question.Id = insertedId;
            return question;
        }

        public bool Update(Question question)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "Update");
            parms.Add("@Id", question.Id);
            parms.Add("@FeedbackQuestion", question.FeedbackQuestion);
            parms.Add("@ModifiedBy", question.ModifiedBy);
            parms.Add("@ModifiedOn", question.ModifiedOn = DateTime.Now);

            var result = _unitOfWork.Questions.Update(StoreProcedure, parms);

            return result;
        }

        public Question GetById(int questionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "GetById");
            parms.Add("@Id", questionId);

            var question = _unitOfWork.Questions.GetById(StoreProcedure, parms);

            return question;
        }
    }
}
