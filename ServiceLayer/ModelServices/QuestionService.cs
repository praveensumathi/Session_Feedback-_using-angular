using AutoMapper;
using BAL.ViewModels;
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
        private readonly IMapper _mapper;

        public QuestionService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<QuestionViewModel> GetQuestionsBySId(int sessionId)
        {
            var questions = _unitOfWork.Questions.GetQuestionsBySId(sessionId);
            _unitOfWork.Commit();

            return _mapper.Map<IEnumerable<QuestionViewModel>>(questions);
        }

        public QuestionViewModel GetById(int questionId)
        {
            var question = _unitOfWork.Questions.GetByQId(questionId);
            _unitOfWork.Commit();

            return _mapper.Map<QuestionViewModel>(question);
        }

        public QuestionViewModel Insert(QuestionViewModel questionViewModel)
        {
            var question = _mapper.Map<Question>(questionViewModel);

            var newQuestion = _unitOfWork.Questions.Create(question);
            _unitOfWork.Commit();

            return _mapper.Map<QuestionViewModel>(newQuestion);
        }

        public bool Update(QuestionViewModel questionViewModel)
        {
            var question = _mapper.Map<Question>(questionViewModel);

            var result = _unitOfWork.Questions.UpdateQuestion(question);
            _unitOfWork.Commit();

            return result;
        }
    }
}
