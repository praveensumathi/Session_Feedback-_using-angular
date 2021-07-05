using Dapper;
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
    public class AnswerController : ControllerBase
    {
        private readonly string connectionString;
        private readonly DapperAnswerRepository _dapperAnswerRepository;

        public AnswerController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connection_string");
            _dapperAnswerRepository = new DapperAnswerRepository(connectionString);
        }

        [HttpGet]
        public IActionResult Get()
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@StatementType", "SelectAll");

            var answers = _dapperAnswerRepository.GetAll("",parms);
            return Ok(answers);
        }

    }
}
