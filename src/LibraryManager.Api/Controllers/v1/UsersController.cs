using AutoMapper;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryManager.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _booksRepository;
        public readonly IMapper _mapper;

        public UsersController(IUsersRepository usersRepository, IMapper mapper) : base()
        {
            _booksRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<PaginatedOutput<UserOutputDto>> Get([FromQuery] Pagination pagination)
        {
            var users = _booksRepository.GetAllPaginated(pagination ?? new Pagination());
            var usersOutput = _mapper.Map<IEnumerable<UserOutputDto>>(users);
            var paginatedResult = new PaginatedOutput<UserOutputDto>(pagination, usersOutput);
            return Ok(paginatedResult);
        }

        [HttpGet("{id}")]
        public ActionResult<UserOutputDto> Get(long id)
        {
            var user = _booksRepository.Get(id);
            var userDto = _mapper.Map<UserOutputDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public ActionResult<UserOutputDto> Post([FromBody] UserInputDto userInputDto)
        {
            var userToBeAdded = _mapper.Map<User>(userInputDto);
            var userAdded = _booksRepository.Insert(userToBeAdded);
            var userAddedDto = _mapper.Map<UserOutputDto>(userAdded);
            return StatusCode(201, userAddedDto);
        }

        [HttpPut("{id}")]
        public ActionResult<UserOutputDto> Put(long id, [FromBody] UserInputDto userInputDto)
        {
            var userToBeUpdated = _booksRepository.Get(id);
            var userUpdated = _booksRepository.Update(userToBeUpdated);
            var userUpdatedDto = _mapper.Map<UserOutputDto>(userUpdated);
            return Ok(userUpdatedDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _booksRepository.Delete(id);
            return NoContent();
        }
    }
}