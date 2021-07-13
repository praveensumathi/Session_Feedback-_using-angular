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
            CreateMap<Session, SessionViewModel>().ReverseMap();
            CreateMap<Question, QuestionViewModel>().ReverseMap();
            CreateMap<Answer, AnswerViewModel>();
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
