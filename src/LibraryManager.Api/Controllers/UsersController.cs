using System;
using System.Collections.Generic;
using LibraryManager.Api.Exceptions;
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

        public UsersController(ICrudRepository crudRepository) : base() 
        {
            _crudRepository = crudRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get() => Ok(_crudRepository.GetAll<User>());

        [HttpGet("{id}")]
        public ActionResult<User> Get(long id)
        {
            return Ok(_crudRepository.Get<User>(id));
        } 

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            return StatusCode(201, _crudRepository.Insert(user));
        }

        [HttpPut("{id}")]
        public ActionResult<User> Put(long id, [FromBody] User user)
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