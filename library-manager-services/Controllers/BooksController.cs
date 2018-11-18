using System;
using System.Collections.Generic;
using LibraryManager.Exceptions;
using LibraryManager.Models.Entities;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ICrudRepository _crudRepository;

        public BooksController(ICrudRepository crudRepository) : base() 
        {
            _crudRepository = crudRepository;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_crudRepository.GetAll<Book>());

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            return Ok(_crudRepository.Get<Book>(id));
        } 

        [HttpPost]
        public IActionResult Post([FromBody] Book Book)
        {
            return StatusCode(201 , _crudRepository.Insert(Book));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book Book)
        {
            return Ok(_crudRepository.Update(id, Book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _crudRepository.Delete<Book>(id);
            return NoContent();
        }
    }
}