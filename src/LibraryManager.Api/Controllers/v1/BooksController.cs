using AutoMapper;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace LibraryManager.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public BooksController(IBooksRepository crudRepository, IMapper mapper) : base()
        {
            _booksRepository = crudRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<PaginatedOutput<BookOutputDto>> Get([FromQuery] Pagination pagination)
        {
            var books = _booksRepository.GetAllPaginated(pagination ?? new Pagination());
            var booksOutputDto = _mapper.Map<IEnumerable<BookOutputDto>>(books);
            var paginatedOutput = new PaginatedOutput<BookOutputDto>(pagination, booksOutputDto);
            return Ok(paginatedOutput);
        }

        [HttpGet("{id}")]
        public ActionResult<BookOutputDto> Get(long id)
        {
            var book = _booksRepository.Get(id);
            var outputDto = _mapper.Map<BookOutputDto>(book);
            return Ok(outputDto);
        }

        [HttpPost]
        public ActionResult<BookOutputDto> Post([FromBody] BookInputDto bookInputDto)
        {
            var bookToBeAdded = _mapper.Map<Book>(bookInputDto);
            var bookAdded = _booksRepository.Insert(bookToBeAdded);
            var bookAddedOutputDto = _mapper.Map<BookOutputDto>(bookAdded);
            return StatusCode((int)HttpStatusCode.Created, bookAddedOutputDto);
        }

        [HttpPut("{id}")]
        public ActionResult<BookOutputDto> Put(long id, [FromBody] BookInputDto bookInputDto)
        {
            var bookToBeUpdated = _booksRepository.Get(id);

            bookToBeUpdated.Author = bookInputDto.Author;
            bookToBeUpdated.Description = bookInputDto.Description;
            bookToBeUpdated.Title = bookInputDto.Title;

            var updatedBook = _booksRepository.Update(bookToBeUpdated);
            var updatedBookDto = _mapper.Map<BookOutputDto>(updatedBook);
            return Ok(updatedBookDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _booksRepository.Delete(id);
            return NoContent();
        }
    }
}