using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.Models;

namespace Session_Feedback.core.Repositories
{
    public class SessionRepository:GenericRepository<Session>
    {
        public SessionRepository():base(Helper.OpenSession())
        {

        }
    }
}
