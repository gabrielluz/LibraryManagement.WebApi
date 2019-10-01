using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers.v1
{
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthenticationController : ControllerBase
    {
        private IUsersRepository _usersRepository;
        private ISecurityManager _securityManager;

        public AuthenticationController(IUsersRepository usersRepository, ISecurityManager securityManager) : base()
        {
            _usersRepository = usersRepository;
            _securityManager = securityManager;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthenticationInputDto authenticationDto)
        {
            var credentials = new Credentials(authenticationDto.Email, authenticationDto.Password);
            _usersRepository.Insert(credentials);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public ActionResult<UserOutputDto> Authenticate([FromBody] AuthenticationInputDto authenticationDto)
        {
            var credentials = new Credentials(authenticationDto.Email, authenticationDto.Password);
            var user = _usersRepository.Authenticate(credentials);
            
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials." });
            
            var token = _securityManager.GenerateToken(user);
            return Ok(token);
        }
    }
}
