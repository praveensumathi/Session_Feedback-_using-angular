using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Session_Feedback.core.DapperModelRepositories;
using Session_Feedback.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session_Feedback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly string connectionString;
        private readonly DapperQuestionRepository _dapperQuestionRepository;
        public QuestionController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connection_string");
            _dapperQuestionRepository = new DapperQuestionRepository(connectionString);
            //_dapperSessionRepository = new DapperSessionRepository(connectionString);
        }

        [HttpGet]
        public IActionResult Get()
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectAll");

            var questions = _dapperQuestionRepository.GetQuestionAnswers("Question",parms);
            return Ok(questions);
        }

        [HttpGet("{sessionId}")]
        public IActionResult Get(int sessionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectBySId");
            parms.Add("@SessionId", sessionId);

            var questions = _dapperQuestionRepository.GetQuestionAnswers("Question", parms);
            return Ok(questions);
        }
    }
}
