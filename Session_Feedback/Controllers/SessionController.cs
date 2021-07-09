using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;

namespace Session_Feedback.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sessions = _mapper.Map<IEnumerable<SessionDTO>>(_sessionService.GetAll());
            return Ok(sessions);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Session session)
        {
            var newSession = _sessionService.InsertWithQuestions(session);
            return Ok(newSession);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Session session)
        {
            bool isUpdated = _sessionService.Update(session);
            if (isUpdated)
            {
                return Ok(isUpdated);
            }
            return Ok(false);
        }
    }
}
