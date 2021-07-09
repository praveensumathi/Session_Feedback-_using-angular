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
        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Question> GetQuestionsBySId(int sessionId)
        {
            var questions = _unitOfWork.Questions.GetQuestionsBySId(sessionId);
            _unitOfWork.Commit();

            return questions;
        }

        public Question GetById(int questionId)
        {
            var question = _unitOfWork.Questions.GetByQId(questionId);
            _unitOfWork.Commit();

            return question;
        }

        public Question Insert(Question question)
        {
            var newQuestion = _unitOfWork.Questions.Create(question);
            _unitOfWork.Commit();
            
            return newQuestion;
        }

        public bool Update(Question question)
        {
            
            var result = _unitOfWork.Questions.UpdateQuestion(question);
            _unitOfWork.Commit();

            return result;
        }
    }
}
