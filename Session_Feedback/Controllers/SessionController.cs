using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Session_Feedback.core.DapperModelRepositories;
using Session_Feedback.core.Models;
using Session_Feedback.core.UnitOfWorks;
using System;
using System.Collections.Generic;

namespace Session_Feedback.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly string connectionString;
        private readonly DapperSessionRepository _dapperSessionRepository;
        private readonly DapperQuestionRepository _dapperQuestionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SessionController(IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            connectionString = configuration.GetConnectionString("connection_string");
            _dapperSessionRepository = new DapperSessionRepository(connectionString);
            _dapperQuestionRepository = new DapperQuestionRepository(connectionString);
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectAll");

            //var data = _dapperSessionRepository.GetAll("Session",parms);
            var d = _unitOfWork.Sessions.GetAll("Session", parms);
            _unitOfWork.Commit();
            return Ok(d);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Session session)
        {
            //var newSession = _dapperSessionRepository.InsertSessionWithBulkQuestions(session);
            session.CreatedOn = DateTime.Now;
            var news = _unitOfWork.Sessions.InsertSessionWithBulkQuestions(session);
            _unitOfWork.Commit();
            return Ok(news);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Session session)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", session.SessionId);
            parms.Add("@Name", session.Name);
            parms.Add("@StatementType", "Update");

            bool isUpdated = _dapperSessionRepository.Update("Session",parms);
            if(isUpdated)
            {
                return Ok(isUpdated);
            }
            return Ok(false);
        }
    }
}
