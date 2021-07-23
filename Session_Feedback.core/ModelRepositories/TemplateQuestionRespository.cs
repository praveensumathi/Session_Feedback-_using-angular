using DAL.Models;
using Session_Feedback.core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelRepositories
{
    public class TemplateQuestionRespository : GenericRepo<TemplateQuestion>
    {
        public TemplateQuestionRespository(IDbTransaction dbTransaction) : base(dbTransaction)
        {

        }
    }
}
