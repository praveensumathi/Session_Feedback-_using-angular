using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Session_Feedback.core.DapperModelRepositories;
using Session_Feedback.core.Models;
using Session_Feedback.core.Repositories;
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
        private readonly string connectionString;
        private readonly DapperQuestionRepository _dapperQuestionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionController(IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            connectionString = configuration.GetConnectionString("connection_string");
            _dapperQuestionRepository = new DapperQuestionRepository(connectionString);
            _unitOfWork = unitOfWork;

        }

        //api/question/15
        [HttpGet("{sessionId}")]
        public IActionResult Get(int sessionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectBySId");
            parms.Add("@SessionId", sessionId);

            //var questions = _dapperQuestionRepository.GetQuestionsBySId("Question", parms);
            var questions = _unitOfWork.Questions.GetQuestionsBySId("Question", parms);
            _unitOfWork.Commit();
            return Ok(questions);
        }

        //[HttpGet("[action]")]
        //public IActionResult GetByQId(long QuestionId)
        //{
        //    DynamicParameters parms = new DynamicParameters();
        //    parms.Add("@Id", QuestionId);
        //    parms.Add("@StatementType", "SelectByQId");

        //    var answers = _dapperQuestionRepository.GetQuestionAnswersById("Question", parms);
        //    return Ok(answers);
        //}

        [HttpPost]
        public IActionResult Post([FromBody] Question question)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "Insert");
            parms.Add("@FeedbackQuestion", question.FeedbackQuestion);
            parms.Add("@CreatedBy", question.CreatedBy);
            parms.Add("@CreatedOn", DateTime.Now);
            parms.Add("@SessionId", question.SessionId);

            //var id = _dapperQuestionRepository.Insert("Question", parms);
            var id = _unitOfWork.Questions.Insert("Question", parms);
            _unitOfWork.Commit();
            question.Id = id;
            return Ok(question);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Question question)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", question.Id);
            parms.Add("@FeedbackQuestion", question.FeedbackQuestion);
            parms.Add("@StatementType", "Update");

            bool isUpdated = _dapperQuestionRepository.Update("Question", parms);
            if (isUpdated)
            {
                return Ok(isUpdated);
            }
            return Ok(false);
        }

        //[HttpDelete]
        //public IActionResult Delete(long QuestionId)
        //{

        //}
    }
}
