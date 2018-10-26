using System;
using System.Collections.Generic;
using LibraryManager.Exceptions;
using LibraryManager.Models;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Repository<Book> _booksRepository;

        public BooksController() : base() 
        {
            _booksRepository = new Repository<Book>();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_booksRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            try 
            {
                return Ok(_booksRepository.Get(id));
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
            return Created(uriBuilder.Uri, _booksRepository.Insert(Book));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book Book)
        {
            try 
            {
                return Ok(_booksRepository.Update(id, Book));
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
                _booksRepository.Delete(id);
                return Ok();
            }
            catch (EntityNotFoundException<Book> ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}