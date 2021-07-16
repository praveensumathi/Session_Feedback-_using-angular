using Dapper;
using Session_Feedback.core.Models;
using Session_Feedback.core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace Session_Feedback.core.ModelRepositories
{
    public class SessionRepository : GenericRepo<Session>
    {
        public SessionRepository(IDbTransaction dbTransaction) : base(dbTransaction)
        {
        }

        private readonly string SessionTableName = "Session";
        private readonly string QuestionTableName = "Question";
        private readonly string StoreProcedure = "usp_Session";

        public IEnumerable<Session> GetAllSession()
        {
            var parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectAll");

            var sessions = GetAll(StoreProcedure, parms);

            return sessions;
        }

        public Session GetBySessionId(int sessionId)
        {
            var parms = new DynamicParameters();
            parms.Add("@StatementType", "GetById");
            parms.Add("@Id", sessionId);

            var session = GetByIdOrName(StoreProcedure, parms);

            return session;
        }

        public Session Create(Session session)
        {
            var parms = new DynamicParameters();
            parms.Add("@Name", session.Name);
            parms.Add("@CreatedBy", session.CreatedBy);
            parms.Add("@CreatedOn", DateTime.Now);
            parms.Add("@ConductedBy", session.ConductedBy);
            parms.Add("@StatementType", "Insert");

            var insetedId = Insert(StoreProcedure, parms);

            session.Id = insetedId;

            return session;
        }

        public bool UpdateSession(Session session)
        {
            var parms = new DynamicParameters();
            parms.Add("@Id", session.Id);
            parms.Add("@Name", session.Name);
            parms.Add("@ModifiedBy", session.ModifiedBy);
            parms.Add("@ModifiedOn", DateTime.Now);
            parms.Add("@ConductedBy", session.ConductedBy);
            parms.Add("@StatementType", "Update");

            var isUpdated = Update(StoreProcedure, parms);

            return isUpdated;
        }

        public bool DeleteSession(int id)
        {
            var parms = new DynamicParameters();
            parms.Add("@Id", id);

            var isDeleted = Delete(StoreProcedure, parms);

            return isDeleted;
        }
        public Session InsertSessionWithBulkQuestions(Session session)
        {
            session.CreatedOn = DateTime.Now;

            DapperPlusManager.Entity<Session>().Table(SessionTableName).Identity(x => x.Id);
            DapperPlusManager.Entity<Question>().Table(QuestionTableName).Identity(x => x.Id);

            List<Session> sessions = new List<Session>() { session };

            Connection.UseBulkOptions(options => options.Transaction = (System.Data.Common.DbTransaction)Transaction)
                .BulkInsert<Session>(sessions)
                .ThenForEach(s => s.Questions.ForEach(q =>
                {
                    q.SessionId = s.Id;
                    q.CreatedOn = DateTime.Now;
                }))
                .ThenBulkInsert(s => s.Questions);

            return session;
        }

        //Not Used this method (only for reference)
        public IEnumerable<Session> GetAllSessionQuestion(string sp, DynamicParameters parms)
        {
            var sessionDictionary = new Dictionary<int, Session>();
            var questionDictionary = new Dictionary<int, Question>();

            var result = Connection.Query<Session, Question, Session>(sp, (s, q) =>
            {
                Session session;
                Question question;

                if (!(sessionDictionary.TryGetValue(s.Id, out session)))
                {
                    session = s;
                    session.Questions = new List<Question>();
                    sessionDictionary.Add(s.Id, s);
                }
                if (!(questionDictionary.TryGetValue(q.Id, out question)))
                {
                    question = q;
                    question.Answers = new List<Answer>();
                    questionDictionary.Add(q.Id, q);
                }
                session.Questions.Add(q);
                return session;

            }, splitOn: "QuestionId", param: parms, commandType: CommandType.StoredProcedure).Distinct().ToList();

            return result;
        }
    }
}
