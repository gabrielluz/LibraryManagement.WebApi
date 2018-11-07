using System;
using System.Collections.Generic;
using LibraryManager.Exceptions;
using LibraryManager.Models.Dto;
using LibraryManager.Models.Entities;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class ReviewsController : ApiController
    {
        private readonly IReviewRepository _reviewsRepository;

        public ReviewsController(IReviewRepository reviewRepository) : base() 
        {
            _reviewsRepository = reviewRepository;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_reviewsRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            try 
            {
                return Ok(_reviewsRepository.Get(id));
            }
            catch (EntityNotFoundException<Review> ex)
            {
                return NotFound(new { ex.Message });
            }
        } 

        [HttpPost]
        public IActionResult Post([FromBody] ReviewDto review)
        {
            try 
            {
                var uriBuilder = new UriBuilder()
                {
                    Host = this.HttpContext.Request.Host.Host
                };
                return Created(uriBuilder.Uri, _reviewsRepository.Insert(review));
            }
            catch (EntityNotFoundException<User> ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (EntityNotFoundException<Book> ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReviewDto review)
        {
            try 
            {
                return Ok(_reviewsRepository.Update(id, review));
            }
            catch(EntityNotFoundException<Review> ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try 
            {
                _reviewsRepository.Delete(id);
                return Ok();
            }
            catch (EntityNotFoundException<Review> ex)
            {
                return NotFound(new { ex.Message });
            }
        }
    }
}