using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.DapperRepository;
using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Feedback.core.DapperModelRepositories
{
    public class DapperQuestionRepository :  DRepository<Question>
    {
        public DapperQuestionRepository(string connectionString):base(Helper.OpenSession(connectionString))
        {

        }
    }
}
