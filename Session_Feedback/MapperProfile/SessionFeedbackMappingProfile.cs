using AutoMapper;
using BAL.ViewModels;
using RepositoryLayer.Models;
using Session_Feedback.core.Models;

namespace API.MapperProfile
{
    public class SessionFeedbackMappingProfile : Profile
    {
        public SessionFeedbackMappingProfile()
        {
            CreateMap<Session, SessionViewModel>();
            CreateMap<Question, QuestionViewModel>();
            CreateMap<Answer, AnswerViewModel>();
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
