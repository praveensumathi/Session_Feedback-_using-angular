using Dapper;
using Session_Feedback.core.Models;
using Session_Feedback.core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string StoreProcedure = "Session";
        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Session> GetAll()
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectAll");

            var sessions = _unitOfWork.Sessions.GetAll(StoreProcedure, parms);
            _unitOfWork.Commit();

            return sessions;
        }

        public Session GetById(int sessionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "GetById");
            parms.Add("@Id", sessionId);

            var session = _unitOfWork.Sessions.GetById(StoreProcedure, parms);
            _unitOfWork.Commit();
            return session;
        }

        public Session Insert(Session session)
        {
            session.CreatedOn = DateTime.Now;
            var newSession = _unitOfWork.Sessions.InsertSessionWithBulkQuestions(session);
            _unitOfWork.Commit();

            return newSession;
        }

        public bool Update(Session session)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", session.Id);
            parms.Add("@Name", session.Name);
            parms.Add("@StatementType", "Update");

            var isUpdated = _unitOfWork.Sessions.Update(StoreProcedure, parms);

            _unitOfWork.Commit();
            return isUpdated;
        }
    }
}
