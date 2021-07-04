using Dapper;
using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Session_Feedback.core.Repositories
{
    public class QuestionRepostory:GenericRepository<Question>
    {
        public QuestionRepostory(string connectionString):base(Helper.OpenSession(connectionString))
        {

        }


        public IEnumerable<Session> GetByQuery()
        {
            string sql = "SELECT * FROM OrderDetails";

            var sessions = Session.Query<Session>(sql).ToList();

            return sessions;
        }
    }
}
