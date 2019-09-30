using AutoMapper;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryManager.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : ControllerBase
    {
        private readonly ICrudRepository _crudRepository;
        public readonly IMapper _mapper;

        public UsersController(ICrudRepository crudRepository, IMapper mapper) : base()
        {
            _crudRepository = crudRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserOutputDto>> Get()
        {
            var users = _crudRepository.GetAll<User>();
            var usersOutput = _mapper.Map<IEnumerable<UserOutputDto>>(users);
            return Ok(usersOutput);
        }

        [HttpGet("{id}")]
        public ActionResult<UserOutputDto> Get(long id)
        {
            var user = _crudRepository.Get<User>(id);
            var userDto = _mapper.Map<UserOutputDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public ActionResult<UserOutputDto> Post([FromBody] UserInputDto userInputDto)
        {
            var userToBeAdded = _mapper.Map<User>(userInputDto);
            var userAdded = _crudRepository.Insert(userToBeAdded);
            var userAddedDto = _mapper.Map<UserOutputDto>(userAdded);
            return StatusCode(201, userAddedDto);
        }

        [HttpPut("{id}")]
        public ActionResult<UserOutputDto> Put(long id, [FromBody] UserInputDto userInputDto)
        {
            var userToBeUpdated = _crudRepository.Get<User>(id);
            var userUpdated = _crudRepository.Update(userToBeUpdated);
            var userUpdatedDto = _mapper.Map<UserOutputDto>(userUpdated);
            return Ok(userUpdatedDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _crudRepository.Delete<User>(id);
            return NoContent();
        }
    }
}