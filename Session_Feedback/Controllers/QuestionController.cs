using BAL.ViewModels;
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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
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
        public IActionResult Post([FromBody] QuestionViewModel questionViewModel)
        {
            if (questionViewModel is null)
            {
                return BadRequest();
            }

            var newQuestion = _questionService.Insert(questionViewModel);
            return Ok(newQuestion);
        }

        [HttpPut]
        public IActionResult Put([FromBody] QuestionViewModel questionViewModel)
        {
            if (questionViewModel is null)
            {
                return BadRequest();
            }

            bool isUpdated = _questionService.Update(questionViewModel);
            return Ok(isUpdated);
        }

        //[HttpDelete]
        //public IActionResult Delete(long QuestionId)
        //{

        //}
    }
}
