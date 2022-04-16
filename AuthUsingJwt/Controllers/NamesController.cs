using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthUsingJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NamesController : ControllerBase
    {
        private readonly IJwtAuthenticateManager jwtAuthenticateManager;
        public NamesController(IJwtAuthenticateManager jwtAuthenticateManager)
        {
            this.jwtAuthenticateManager = jwtAuthenticateManager;
        }

        // GET: api/<NamesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "New Jersey", "Washington D.C." };
        }

        // GET api/<NamesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserCredential credential)
        {
            var token = jwtAuthenticateManager.Authenticate(credential.Name, credential.Password);

            if(token == null)
            {
                return Unauthorized();
            }

            return Ok(token); 
        }


    }
}
