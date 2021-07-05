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

            var data = _dapperSessionRepository.GetAllSessionQuestion("Session",parms);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Session session)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Name", session.Name);
            parms.Add("@CreatedBy", session.CreatedBy);
            parms.Add("@CreatedOn", session.CreatedOn);
            parms.Add("@StatementType", "Insert");

            var insertedId = _dapperSessionRepository.Insert("Session", parms);
            session.SessionId = insertedId;
            if(insertedId != null)
            {
                var AffectedRow = _dapperQuestionRepository.Insert
            }

            return Ok(session);
        }
    }
}
