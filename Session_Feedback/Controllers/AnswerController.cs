using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Session_Feedback.core.DapperModelRepositories;
using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session_Feedback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly string connectionString;
        private readonly DapperAnswerRepository _dapperAnswerRepository;

        public AnswerController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connection_string");
            _dapperAnswerRepository = new DapperAnswerRepository(connectionString);
        }

        //api/answer/2
        [HttpGet("{questionId}")]
        public IActionResult GetAnswersByQId(long QuestionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@QuestionId", QuestionId);
            parms.Add("@StatementType", "SelectByQId");

            var answers = _dapperAnswerRepository.GetQuestionAnswersByQId("Answer", parms);
            return Ok(answers);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Answer answer)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@QuestionAnswer", answer.QuestionAnswer);
            parms.Add("@AnsweredBy", answer.AnsweredBy);
            parms.Add("@AnsweredOn", DateTime.Now);
            parms.Add("@QuestionId", answer.QuestionId);
            parms.Add("@StatementType", "Insert");

            var id = _dapperAnswerRepository.Insert("Answer", parms);

            answer.AnswerId = id;
            return Ok(answer);
        }
    }
}
