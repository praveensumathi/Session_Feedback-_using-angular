using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Session_Feedback.core.DapperModelRepositories;
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
        }

        [HttpGet]
        public IActionResult Get()
        {
            var questions = _dapperQuestionRepository.GetAll("");
            return Ok(questions);
        }
    }
}
