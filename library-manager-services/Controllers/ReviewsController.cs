using System;
using System.Collections.Generic;
using LibraryManager.Exceptions;
using LibraryManager.Models.Dto;
using LibraryManager.Models.Entities;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    [Route("/api/books")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewsRepository;

        public ReviewsController(IReviewRepository reviewRepository) : base() 
        {
            _reviewsRepository = reviewRepository;
        }

        [HttpGet]
        [Route("{bookId}/reviews")]
        public IActionResult Get() => Ok(_reviewsRepository.GetAll());

        [HttpGet("{reviewId}")]
        public IActionResult Get(int id) 
        {
            return Ok(_reviewsRepository.Get(id));
        } 

        [HttpPost]
        [Route("{bookId}/reviews")]
        public IActionResult Post([FromBody] ReviewDto review)
        {
            return StatusCode(201, _reviewsRepository.Insert(review));
        }

        [HttpPut("{reviewId}")]
        public IActionResult Put(int reviewId, [FromBody] ReviewDto review)
        {
            return Ok(_reviewsRepository.Update(reviewId, review));
        }

        [HttpDelete("{reviewId}")]
        public IActionResult Delete(int reviewId)
        {
            _reviewsRepository.Delete(reviewId);
            return NoContent();
        }
    }
}