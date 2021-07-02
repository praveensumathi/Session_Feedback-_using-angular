using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.Models;

namespace Session_Feedback.core.Repositories
{
    class QuestionRepository : GenericRepository<Question>
    {
        public QuestionRepository(string connectionString) : base(Helper.OpenSession(connectionString))
        {

        }
    }
}
