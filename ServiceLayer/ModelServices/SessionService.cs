using Dapper;
using Session_Feedback.core.Models;
using Session_Feedback.core.UnitOfWorks;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Session> GetAll()
        {
            var sessions = _unitOfWork.Sessions.GetAllSession();
            _unitOfWork.Commit();

            return sessions;
        }

        public Session GetById(int sessionId)
        {
            var session = _unitOfWork.Sessions.GetBySessionId(sessionId);
            _unitOfWork.Commit();

            return session;
        }

        public Session InsertWithQuestions(Session session)
        {
            var newSession = _unitOfWork.Sessions.InsertSessionWithBulkQuestions(session);
            _unitOfWork.Commit();

            return newSession;
        }

        public bool Update(Session session)
        {
            var isUpdated = _unitOfWork.Sessions.UpdateSession(session);
            _unitOfWork.Commit();

            return isUpdated;
        }
    }
}
