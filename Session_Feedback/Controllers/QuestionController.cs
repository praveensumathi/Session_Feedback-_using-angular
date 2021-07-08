using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Interfaces;
using Session_Feedback.core.DapperModelRepositories;
using Session_Feedback.core.Models;
using Session_Feedback.core.UnitOfWorks;
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
        private readonly IQuestionService _questionService;

        public QuestionController(IConfiguration configuration,IUnitOfWork unitOfWork,IQuestionService questionService)
        {
            _questionService = questionService;
        }

        //api/question/15
        [HttpGet("{sessionId}")]
        public IActionResult Get(int sessionId)
        {
            var questions = _questionService.GetQuestionsBySId(sessionId);
            return Ok(questions);
        }

        [HttpGet("[action]")]
        public IActionResult GetByQId(int questionId)
        {
            var question = _questionService.GetById(questionId);
            return Ok(question);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Question question)
        {
            var newQuestion = _questionService.Insert(question);
            return Ok(newQuestion);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Question question)
        {
           
            bool isUpdated = _questionService.Update(question);
            return Ok(isUpdated);
        }

        //[HttpDelete]
        //public IActionResult Delete(long QuestionId)
        //{

        //}
    }
}
