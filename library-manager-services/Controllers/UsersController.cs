using System;
using System.Collections.Generic;
using LibraryManager.Exceptions;
using LibraryManager.Models.Entities;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class UsersController : ApiController
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
            try 
            {
                return Ok(_crudRepository.Get<User>(id));
            }
            catch (EntityNotFoundException<User> ex)
            {
                return NotFound(new { ex.Message });
            }
        } 

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var uriBuilder = new UriBuilder()
            {
                Host = this.HttpContext.Request.Host.Host
            };
            return Created(uriBuilder.Uri, _crudRepository.Insert(user));
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            try 
            {
                return Ok(_crudRepository.Update(id, user));
            }
            catch(EntityNotFoundException<User> ex)
            {
                return NotFound(new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try 
            {
                _crudRepository.Delete<User>(id);
                return Ok();
            }
            catch (EntityNotFoundException<User> ex)
            {
                return NotFound(new { ex.Message });
            }
        }
    }
}