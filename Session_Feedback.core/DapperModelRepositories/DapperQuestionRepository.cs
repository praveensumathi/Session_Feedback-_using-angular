using Dapper;
using Session_Feedback.core.ConnectionHelper;
using Session_Feedback.core.DapperRepository;
using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Feedback.core.DapperModelRepositories
{
    public class DapperQuestionRepository : DRepository<Question>
    {

        public DapperQuestionRepository(string connectionString) : base(Helper.OpenSession(connectionString))
        {

        }

        public IEnumerable<Question> GetAllQuestionAnswers(string sp, DynamicParameters parms)
        {
            var questionDictionary = new Dictionary<int, Question>();

            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();

            var result = DbConnection.Query<Question, Answer, Question>(sp, (q, a) =>
               {
                   Question question;

                   if (!(questionDictionary.TryGetValue(q.QuestionId, out question)))
                   {
                       question = q;
                       question.Answers = new List<Answer>();
                       questionDictionary.Add(q.QuestionId, q);
                   }
                   question.Answers.Add(a);
                   return question;
               }, splitOn: "SessionId", param: parms, commandType: CommandType.StoredProcedure).Distinct().ToList();

            return result;
        }
    }
}
