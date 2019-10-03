using AutoMapper;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public readonly IMapper _mapper;

        public AuthenticationController(IUsersRepository usersRepository, ISecurityManager securityManager, IMapper mapper) : base()
        {
            _usersRepository = usersRepository;
            _securityManager = securityManager;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthenticationInputDto authenticationDto)
        {
            var credentials = new Credentials(authenticationDto.Email, authenticationDto.Password);
            var user = _usersRepository.Insert(credentials);
            var userOutputDto = _mapper.Map<UserRegisteredOutputDto>(user);
            return StatusCode((int)HttpStatusCode.Created, userOutputDto);
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
