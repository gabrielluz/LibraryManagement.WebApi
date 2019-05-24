using System;
using System.Collections.Generic;
using LibraryManagerApi.Exceptions;
using LibraryManagerApi.Models.Entities;
using LibraryManagerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerApi.Controllers
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
        public ActionResult<Book> Get() => Ok(_crudRepository.GetAll<Book>());

        [HttpGet("{id}")]
        public ActionResult<Book> Get(long id) 
        {
            return Ok(_crudRepository.Get<Book>(id));
        } 

        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book Book)
        {
            return StatusCode(201 , _crudRepository.Insert(Book));
        }

        [HttpPut("{id}")]
        public ActionResult<Book> Put(long id, [FromBody] Book Book)
        {
            return Ok(_crudRepository.Update(id, Book));
        }

        [HttpDelete("{id}")]
        public ActionResult<Book> Delete(long id)
        {
            _crudRepository.Delete<Book>(id);
            return NoContent();
        }
    }
}