using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Session_Feedback.core.DapperModelRepositories;
using Session_Feedback.core.Models;
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

        public SessionController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connection_string");
            _dapperSessionRepository = new DapperSessionRepository(connectionString);
            _dapperQuestionRepository = new DapperQuestionRepository(connectionString);
        }

        [HttpGet]
        public IActionResult Get()
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectAll");

            var data = _dapperSessionRepository.GetAll("Session",parms);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Session session)
        {
            var newSession = _dapperSessionRepository.InsertSessionWithBulkQuestions(session);
            return Ok(newSession);
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
