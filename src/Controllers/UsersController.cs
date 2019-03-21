using System;
using System.Collections.Generic;
using LibraryManagerApi.Exceptions;
using LibraryManagerApi.Models.Entities;
using LibraryManagerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ICrudRepository _crudRepository;

        public UsersController(ICrudRepository crudRepository) : base() 
        {
            _crudRepository = crudRepository;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_crudRepository.GetAll<User>());

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_crudRepository.Get<User>(id));
        } 

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            return StatusCode(201, _crudRepository.Insert(user));
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            return Ok(_crudRepository.Update(id, user));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _crudRepository.Delete<User>(id);
            return NoContent();
        }
    }
}