using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.Models;

namespace Session_Feedback.core.Repositories
{
    class AnswerRepository : GenericRepository<Answer>
    {
        public AnswerRepository(string connectionString) : base(Helper.OpenSession(connectionString))
        {

        }
    }
}
