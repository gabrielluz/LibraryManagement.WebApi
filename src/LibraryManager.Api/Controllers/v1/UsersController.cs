using AutoMapper;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryManager.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        public readonly IMapper _mapper;

        public UsersController(IUsersRepository usersRepository, IMapper mapper) : base()
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<PaginatedOutput<UserOutputDto>> Get([FromQuery] Pagination pagination)
        {
            var users = _usersRepository.GetAllPaginated(pagination ?? new Pagination());
            var usersOutput = _mapper.Map<IEnumerable<UserOutputDto>>(users);
            var paginatedResult = new PaginatedOutput<UserOutputDto>(pagination, usersOutput);
            return Ok(paginatedResult);
        }

        [HttpGet("{id}")]
        public ActionResult<UserOutputDto> Get(long id)
        {
            var user = _usersRepository.Get(id);
            var userDto = _mapper.Map<UserOutputDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public ActionResult<UserOutputDto> Put(long id, [FromBody] UserInputDto userInputDto)
        {
            var userToBeUpdated = _usersRepository.Get(id);
            var userUpdated = _usersRepository.Update(userToBeUpdated);
            var userUpdatedDto = _mapper.Map<UserOutputDto>(userUpdated);
            return Ok(userUpdatedDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _usersRepository.Delete(id);
            return NoContent();
        }
    }
}