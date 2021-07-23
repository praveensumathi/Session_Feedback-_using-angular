using DAL.ModelRepositories;
using Session_Feedback.core.ModelRepositories;
using Session_Feedback.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Feedback.core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        SessionRepository Sessions { get; }
        QuestionRepository Questions { get; }
        AnswerRepository Answers { get; }
        ApplicationUserRepository Users { get; }

        QuestionTemplateRepository Templates { get; }
        void Commit();
    }
}
