using AutoMapper;
using BAL.ViewModels;
using Dapper;
using Session_Feedback.core.Models;
using Session_Feedback.core.UnitOfWorks;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool Delete(int id)
        {
            var isDeleted = _unitOfWork.Sessions.DeleteSession(id);
            _unitOfWork.Commit();

            return isDeleted;
        }

        public IEnumerable<SessionViewModel> GetAll()
        {
            var sessions = _unitOfWork.Sessions.GetAllSession();
            _unitOfWork.Commit();

            return _mapper.Map<IEnumerable<SessionViewModel>>(sessions);
        }

        public SessionViewModel GetById(int sessionId)
        {
            var session = _unitOfWork.Sessions.GetBySessionId(sessionId);
            _unitOfWork.Commit();

            return _mapper.Map<SessionViewModel>(session);
        }

        public SessionViewModel Insert(SessionViewModel sessionViewModel)
        {
            var session = _mapper.Map<Session>(sessionViewModel);

            var newSession = _unitOfWork.Sessions.Create(session);
            _unitOfWork.Commit();

            return _mapper.Map<SessionViewModel>(newSession);
        }

        public SessionViewModel InsertWithQuestions(Session session)
        {
            var newSession = _unitOfWork.Sessions.InsertSessionWithBulkQuestions(session);
            _unitOfWork.Commit();

            return _mapper.Map<SessionViewModel>(newSession);
        }

        public bool Update(SessionViewModel sessionViewModel)
        {
            var session = _mapper.Map<Session>(sessionViewModel);

            var isUpdated = _unitOfWork.Sessions.UpdateSession(session);
            _unitOfWork.Commit();

            return isUpdated;
        }
    }
}
