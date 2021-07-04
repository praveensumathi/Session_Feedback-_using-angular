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
        private readonly DapperSessionRepository _dapperSessionRepository;
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

            var questions = _dapperQuestionRepository.GetAllQuestionAnswers("Question",parms);
            return Ok(questions);
        }
    }
}
