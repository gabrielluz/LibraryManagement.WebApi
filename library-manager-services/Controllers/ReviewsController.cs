using System;
using System.Collections.Generic;
using AutoMapper;
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
        private readonly ICrudRepository _crudRepository;
        private readonly IMapper _mapper;

        public ReviewsController(
            IReviewRepository reviewRepository, 
            ICrudRepository crudRepository,
            IMapper mapper) : base() 
        {
            _reviewsRepository = reviewRepository;
            _crudRepository = crudRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{bookId}/reviews")]
        public IActionResult Get() => Ok(_reviewsRepository.GetAll());

        [HttpGet("{bookId}/reviews/{reviewId}")]
        public IActionResult Get(int bookId, int reviewId) 
        {
            return Ok(_reviewsRepository.Get(reviewId));
        } 

        [HttpPost]
        [Route("{bookId}/reviews")]
        public IActionResult Post(long bookId, [FromBody] ReviewInputDto reviewDto)
        {
            var review = _mapper.Map<ReviewInputDto, Review>(reviewDto);
            review.User = _crudRepository.Get<User>(reviewDto?.UserId);
            review.Book = _crudRepository.Get<Book>(bookId);
            return StatusCode(201, _reviewsRepository.Insert(review));
        }

        [HttpPut("{reviewId}")]
        public IActionResult Put(int reviewId, [FromBody] ReviewInputDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
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