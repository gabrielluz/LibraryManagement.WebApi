using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
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
            var usersOutput = new Collection<UserOutputDto>();

            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserOutputDto>(user);
                usersOutput.Add(userDto);
            }

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

            userToBeUpdated.FirstName = userInputDto.FirstName;
            userToBeUpdated.LastName = userInputDto.LastName;
            userToBeUpdated.Email = userInputDto.Email;
            userToBeUpdated.Description = userInputDto.Description;

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