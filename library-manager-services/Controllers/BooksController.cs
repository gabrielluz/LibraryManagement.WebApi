using System;
using System.Collections.Generic;
using LibraryManager.Exceptions;
using LibraryManager.Models.Entities;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class BooksController : ApiController
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
            try 
            {
                return Ok(_crudRepository.Get<Book>(id));
            }
            catch (EntityNotFoundException<Book> ex)
            {
                return NotFound(ex.Message);
            }
        } 

        [HttpPost]
        public IActionResult Post([FromBody] Book Book)
        {
            var uriBuilder = new UriBuilder()
            {
                Host = this.HttpContext.Request.Host.Host
            };
            return Created(uriBuilder.Uri, _crudRepository.Insert(Book));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book Book)
        {
            try 
            {
                return Ok(_crudRepository.Update(id, Book));
            }
            catch(EntityNotFoundException<Book> ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try 
            {
                _crudRepository.Delete<Book>(id);
                return Ok();
            }
            catch (EntityNotFoundException<Book> ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}