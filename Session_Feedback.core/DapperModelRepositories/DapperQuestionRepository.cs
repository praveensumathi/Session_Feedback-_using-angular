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
using Z.Dapper.Plus;

namespace Session_Feedback.core.DapperModelRepositories
{
    public class DapperQuestionRepository : DRepository<Question>
    {

        public DapperQuestionRepository(string connectionString) : base(Helper.OpenSession(connectionString))
        {

        }

        public IEnumerable<Question> GetQuestionsBySId(string sp, DynamicParameters parms)
        {
            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();

            var result = DbConnection.Query<Question>(sp, param: parms, commandType: CommandType.StoredProcedure);

            return result;
        }

        public bool DeleteQuestionWithAnswers(long QId)
        {
            DapperPlusManager.Entity<Question>().Table("Questions").Identity(x => x.Id);
            DapperPlusManager.Entity<Answer>().Table("Sessions").Identity(x => x.AnswerId);

            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", QId);
            parms.Add("@StatementType", "SelectByQId");

            var question = GetQuestionAnswersById("Question", parms);

            List<Question> questions = new List<Question>() { question };

            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();

            DbConnection.BulkDelete(questions.SelectMany(q => q.Answers)).BulkDelete(questions);
            return true;
        }

        public Question GetQuestionAnswersById(string sp, DynamicParameters parms)
        {
            var questionDictionary = new Dictionary<int, Question>();

            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();

            var result = DbConnection.Query<Question, Answer, Question>(sp, (q, a) =>
            {
                Question question;

                if (!(questionDictionary.TryGetValue(q.Id, out question)))
                {
                    question = q;
                    question.Answers = new List<Answer>();
                    questionDictionary.Add(q.Id, q);
                }
                question.Answers.Add(a);
                return question;

            }, splitOn: "SessionId", param: parms, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return result;
        }
    }
}
