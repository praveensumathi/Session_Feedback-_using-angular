using Dapper;
using Session_Feedback.core.Models;
using Session_Feedback.core.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace Session_Feedback.core.ModelRepositories
{
    public class QuestionRepository : GenericRepo<Question>
    {
        public QuestionRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public IEnumerable<Question> GetQuestionsBySId(string sp, DynamicParameters parms)
        {
            var questions = Connection.Query<Question>(sp, param : parms, commandType: CommandType.StoredProcedure,transaction:Transaction);

            return questions;
        }

        //this method for delete question with answers
        public bool DeleteQuestionWithAnswers(long QId)
        {
            DapperPlusManager.Entity<Question>().Table("Questions").Identity(x => x.Id);
            DapperPlusManager.Entity<Answer>().Table("Sessions").Identity(x => x.AnswerId);

            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", QId);
            parms.Add("@StatementType", "SelectByQId");

            var question = GetQuestionWithAnswersByQId("Question", parms);

            List<Question> questions = new List<Question>() { question };


            Connection.BulkDelete(questions.SelectMany(q => q.Answers)).BulkDelete(questions);
            return true;
        }

        public Question GetQuestionWithAnswersByQId(string sp, DynamicParameters parms)
        {
            var questionDictionary = new Dictionary<int, Question>();

            
            var result = Connection.Query<Question, Answer, Question>(sp, (q, a) =>
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
