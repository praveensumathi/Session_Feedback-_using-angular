using Dapper;
using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.DapperRepository;
using Session_Feedback.core.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Session_Feedback.core.DapperModelRepositories
{
    public class DapperSessionRepository : DRepository<Session>
    {
        public DapperSessionRepository(string connectionString) :  base(Helper.OpenSession(connectionString))
        {

        }

        public IEnumerable<Session> GetAllSessionQuestion(string sp, DynamicParameters parms)
        {
            var sessionDictionary = new Dictionary<int, Session>();
            var questionDictionary = new Dictionary<int, Question>();

            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();

            var result = DbConnection.Query<Session, Question,Session>(sp, (s, q) =>
            {
                Session session;
                Question question;

                if (!(sessionDictionary.TryGetValue(s.SessionId, out session)))
                {
                    session = s;
                    session.Questions = new List<Question>();
                    sessionDictionary.Add(s.SessionId, s);
                }
                if (!(questionDictionary.TryGetValue(q.QuestionId, out question)))
                {
                    question = q;
                    question.Answers = new List<Answer>();
                    questionDictionary.Add(q.QuestionId, q);
                }
                session.Questions.Add(q);
                return session;

            }, splitOn: "QuestionId", param: parms, commandType: CommandType.StoredProcedure).Distinct().ToList();

            return result;
        }
    }
}
