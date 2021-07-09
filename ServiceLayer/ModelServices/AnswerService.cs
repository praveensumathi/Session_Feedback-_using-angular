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
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnswerService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<AnswerViewModel> GetAnswersByQId(int questionId)
        {
            var answers = _unitOfWork.Answers.GetAnswersByQId(questionId);
            _unitOfWork.Commit();

            return _mapper.Map<IEnumerable<AnswerViewModel>>(answers);
        }

        public IEnumerable<AnswerViewModel> GetAnswersByUIdAndQId(long userId, long questionId)
        {
            throw new NotImplementedException();
        }
    }
}
