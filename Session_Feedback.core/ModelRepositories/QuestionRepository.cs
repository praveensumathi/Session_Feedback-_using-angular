using Dapper;
using Session_Feedback.core.Models;
using Session_Feedback.core.Repositories;
using System;
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

        private readonly string StoreProcedure = "Question";

        public IEnumerable<Question> GetQuestionsBySId(int sessionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectBySId");
            parms.Add("@SessionId", sessionId);

            var questions = GetAll(StoreProcedure, parms);

            return questions;
        }

        public Question GetByQId(int questionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "GetById");
            parms.Add("@Id", questionId);

            var question = GetById(StoreProcedure, parms);

            return question;
        }

        public Question Create(Question question)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "Insert");
            parms.Add("@FeedbackQuestion", question.FeedbackQuestion);
            parms.Add("@CreatedBy", question.CreatedBy);
            parms.Add("@CreatedOn", DateTime.Now);
            parms.Add("@SessionId", question.SessionId);

            var insertedId = Insert(StoreProcedure, parms);

            question.Id = insertedId;

            return question;
        }

        public bool UpdateQuestion(Question question)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "Update");
            parms.Add("@Id", question.Id);
            parms.Add("@FeedbackQuestion", question.FeedbackQuestion);
            parms.Add("@ModifiedBy", question.ModifiedBy);
            parms.Add("@ModifiedOn", question.ModifiedOn = DateTime.Now);

            var isUpdated = Update(StoreProcedure, parms);

            return isUpdated;
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
