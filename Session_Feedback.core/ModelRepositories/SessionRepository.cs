using Session_Feedback.core.Models;
using Session_Feedback.core.Repositories;
using Session_Feedback.core.UnitOfWorks;
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
        private readonly string SessionTableName = "Sessions";
        private readonly string QuestionTableName = "Questions";

        public Session InsertSessionWithBulkQuestions(Session session)
        {
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
    }
}
