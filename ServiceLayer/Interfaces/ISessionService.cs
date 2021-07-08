﻿using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ISessionService
    {
        IEnumerable<Session> GetAll();
        Session Insert(Session session);
        bool Update(Session session);
    }
}