using Dapper;
using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.DapperRepository;
using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Session_Feedback.core.DapperModelRepositories
{
    public class DapperAnswerRepository : DRepository<Answer>
    {
        public DapperAnswerRepository(string connectionString) : base(Helper.OpenSession(connectionString))
        {

        }

        public IEnumerable<Answer> GetQuestionAnswersById(string sp, DynamicParameters parms)
        {
            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();

            var result = DbConnection.Query<Answer>(sp, param: parms, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
