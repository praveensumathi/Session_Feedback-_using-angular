using AutoMapper;
using BAL.Interfaces;
using BAL.ViewModels;
using DAL.Models;
using Session_Feedback.core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ModelServices
{
    public class QuestionTemplateService : IQuestionTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionTemplateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<QuestionTemplateViewModel> GetAll()
        {
            var templates = _unitOfWork.Templates.GetAllTemplate();
            _unitOfWork.Commit();

            return _mapper.Map<IEnumerable<QuestionTemplateViewModel>>(templates);
        }
    }
}
