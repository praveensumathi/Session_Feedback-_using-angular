using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.Models;

namespace Session_Feedback.core.Repositories
{
    class FeedbackRepository : GenericRepository<Feedback>
    {
        public FeedbackRepository():base(Helper.OpenSession())
        {

        }
    }
}
