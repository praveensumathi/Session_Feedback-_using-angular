using DAL.Models;
using Dapper;
using Session_Feedback.core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelRepositories
{
    public class QuestionTemplateRepository : GenericRepo<QuestionTemplate>
    {
        public QuestionTemplateRepository(IDbTransaction dbTransaction) : base(dbTransaction)
        {

        }

        private readonly string StoreProcedure = "usp_QuestionTemplate";

        public IEnumerable<QuestionTemplate> GetAllTemplate()
        {
            var parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectAll");

            var templates = GetAll(StoreProcedure, parms);

            return templates;
        }
    }
}
