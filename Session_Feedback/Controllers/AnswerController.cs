using Dapper;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
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
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        //api/answer/2
        [HttpGet("{questionId}")]
        public IActionResult GetAnswersByQId(int questionId)
        {
            var answers = _answerService.GetAnswersByQId(questionId);
            return Ok(answers);
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] Answer answer)
        //{
        //    DynamicParameters parms = new DynamicParameters();
        //    parms.Add("@QuestionAnswer", answer.QuestionAnswer);
        //    parms.Add("@AnsweredBy", answer.AnsweredBy);
        //    parms.Add("@AnsweredOn", DateTime.Now);
        //    parms.Add("@QuestionId", answer.QuestionId);
        //    parms.Add("@StatementType", "Insert");

        //    var id = _dapperAnswerRepository.Insert("Answer", parms);

        //    answer.AnswerId = id;
        //    return Ok(answer);
        //}
    }
}
