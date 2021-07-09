﻿using AutoMapper;
using BAL.ViewModels;
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
        public IActionResult Post([FromBody] SessionViewModel sessionViewModel)
        {
            if(sessionViewModel == null)
            {
                return BadRequest();
            }

            var newSession = _sessionService.Insert(sessionViewModel);
            return Ok(newSession);
        }

        [HttpPost("action")]
        public IActionResult SessionWithQuestion([FromBody] Session session)
        {
            if (session.Name == null)
            {
                return BadRequest();
            }

            var newSession = _sessionService.InsertWithQuestions(session);
            return Ok(newSession);
        }

        [HttpPut]
        public IActionResult Put([FromBody] SessionViewModel sessionViewModel)
        {
            if (sessionViewModel == null)
            {
                return BadRequest();
            }

            bool isUpdated = _sessionService.Update(sessionViewModel);
            if (isUpdated)
            {
                return Ok(isUpdated);
            }
            return Ok(false);
        }
    }
}
