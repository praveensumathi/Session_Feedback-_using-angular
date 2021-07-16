using BAL.ViewModels;
using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;
namespace ServiceLayer
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAll();
        SessionViewModel GetById(int sessionId);
        SessionViewModel Insert(SessionViewModel sessionViewModel);
        SessionViewModel InsertWithQuestions(Session session);
        bool Update(SessionViewModel session);

        bool Delete(int id);
    }
}
