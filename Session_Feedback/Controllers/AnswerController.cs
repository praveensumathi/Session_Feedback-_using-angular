using BAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
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

        [HttpPost]
        public IActionResult Post([FromBody] AnswerViewModel answerViewModel)
        {
            var result = _answerService.AddAnswer(answerViewModel);
            return Ok(result);
        }
    }
}
