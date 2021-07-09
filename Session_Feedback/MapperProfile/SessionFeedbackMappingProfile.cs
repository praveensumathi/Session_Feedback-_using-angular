using API.Models;
using AutoMapper;
using Session_Feedback.core.Models;

namespace API.MapperProfile
{
    public class SessionFeedbackMappingProfile : Profile
    {
        public SessionFeedbackMappingProfile()
        {
            CreateMap<Session, SessionDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<Answer, AnswerDTO>();
        }
    }
}
