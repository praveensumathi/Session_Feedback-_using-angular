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

        public Session InsertSessionWithBulkQuestions(Session session)
        {
            DapperPlusManager.Entity<Session>().Table("Sessions").Identity(x => x.SessionId);
            DapperPlusManager.Entity<Question>().Table("Questions").Identity(x => x.QuestionId);

            List<Session> sessions = new List<Session>() { session };

            Connection.UseBulkOptions(options => options.Transaction = (System.Data.Common.DbTransaction)Transaction).BulkInsert<Session>(sessions).ThenForEach(s => s.Questions.ForEach(q =>
            {
                q.SessionId = s.SessionId;
                q.CreatedOn = DateTime.Now;
            }
            )).ThenBulkInsert(s => s.Questions);

            return session;
        }
    }
}
