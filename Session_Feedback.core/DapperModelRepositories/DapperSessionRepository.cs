using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.DapperRepository;
using Session_Feedback.core.Models;

namespace Session_Feedback.core.DapperModelRepositories
{
    public class DapperSessionRepository : DRepository<Session>
    {
        public DapperSessionRepository(string connectionString) :  base(Helper.OpenSession(connectionString))
        {

        }
    }
}
