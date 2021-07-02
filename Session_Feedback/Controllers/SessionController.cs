using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Session_Feedback.core.DapperModelRepositories;

namespace Session_Feedback.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        //private readonly SessionRepository _sessionRepository;
        private readonly DapperSessionRepository _dapperSessionRepository;

        private readonly string connectionString;

        public SessionController(IConfiguration configuration)
        {
            
            connectionString = configuration.GetConnectionString("connection_string");
            //_sessionRepository = new SessionRepository(connectionString);
            _dapperSessionRepository = new DapperSessionRepository(connectionString);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _dapperSessionRepository.GetAll("Session");
            return Ok(data);
        }

    }
}
