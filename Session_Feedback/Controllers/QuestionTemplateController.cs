using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTemplateController : ControllerBase
    {
        private readonly IQuestionTemplateService _questionTemplateService;

        public QuestionTemplateController(IQuestionTemplateService questionTemplateService)
        {
            _questionTemplateService = questionTemplateService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var templates = _questionTemplateService.GetAll();

            return Ok(templates);
        }
    }


}
