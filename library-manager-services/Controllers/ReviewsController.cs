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
            return Ok(_reviewsRepository.Get(id));
        } 

        [HttpPost]
        public IActionResult Post([FromBody] ReviewDto review)
        {
            return StatusCode(201, _reviewsRepository.Insert(review));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReviewDto review)
        {
            return Ok(_reviewsRepository.Update(id, review));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _reviewsRepository.Delete(id);
            return NoContent();
        }
    }
}