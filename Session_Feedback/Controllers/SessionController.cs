using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceLayer;
using Session_Feedback.core.Models;
using Session_Feedback.core.UnitOfWorks;
using System;
using System.Collections.Generic;

namespace Session_Feedback.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sessions = _sessionService.GetAll();
            return Ok(sessions);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Session session)
        {
            var newSession = _sessionService.Insert(session);
            return Ok(newSession);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Session session)
        {
            bool isUpdated = _sessionService.Update(session);
            if(isUpdated)
            {
                return Ok(isUpdated);
            }
            return Ok(false);
        }
    }
}
