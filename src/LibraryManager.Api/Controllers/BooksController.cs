using System;
using System.Collections.Generic;
using System.Net;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Linq;
using System.Collections.ObjectModel;

namespace LibraryManager.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ICrudRepository _crudRepository;
        private readonly IMapper _mapper;

        public BooksController(ICrudRepository crudRepository, IMapper mapper) : base() 
        {
            _crudRepository = crudRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookOutputDto>> Get() 
        {
            var books = _crudRepository.GetAll<Book>();
            var booksOutputDto = new Collection<BookOutputDto>();

            foreach (var book in books)
            {
                var bookOutputDto = _mapper.Map<BookOutputDto>(book);
                booksOutputDto.Add(bookOutputDto);
            }

            return Ok(booksOutputDto);
        }

        [HttpGet("{id}")]
        public ActionResult<BookOutputDto> Get(long id) 
        {
            var book = _crudRepository.Get<Book>(id);
            var outputDto = _mapper.Map<BookOutputDto>(book);
            return Ok(outputDto);
        }

        [HttpPost]
        public ActionResult<BookOutputDto> Post([FromBody] BookInputDto bookInputDto)
        {
            var bookToBeAdded = _mapper.Map<Book>(bookInputDto);
            var bookAdded = _crudRepository.Insert(bookToBeAdded);
            var bookAddedOutputDto = _mapper.Map<BookOutputDto>(bookAdded);
            return StatusCode((int)HttpStatusCode.Created, bookAddedOutputDto);
        }

        [HttpPut("{id}")]
        public ActionResult<BookOutputDto> Put(long id, [FromBody] BookInputDto bookInputDto)
        {
            var bookToBeUpdated = _crudRepository.Get<Book>(id);

            bookToBeUpdated.Author = bookInputDto.Author;
            bookToBeUpdated.Description = bookInputDto.Description;
            bookToBeUpdated.Title = bookInputDto.Title;

            var updatedBook = _crudRepository.Update(bookToBeUpdated);
            var updatedBookDto = _mapper.Map<BookOutputDto>(updatedBook);
            return Ok(updatedBookDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _crudRepository.Delete<Book>(id);
            return NoContent();
        }
    }
}