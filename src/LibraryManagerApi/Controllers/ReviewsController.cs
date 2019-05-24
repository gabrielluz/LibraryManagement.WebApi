using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LibraryManagerApi.Exceptions;
using LibraryManagerApi.Models.Dto;
using LibraryManagerApi.Models.Entities;
using LibraryManagerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerApi.Controllers
{
    [Route("/api/books")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewsRepository;
        private readonly ICrudRepository _crudRepository;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewRepository reviewRepository, ICrudRepository crudRepository, IMapper mapper) : base() 
        {
            _reviewsRepository = reviewRepository;
            _crudRepository = crudRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{bookId}/reviews")]
        public ActionResult<IEnumerable<ReviewOutputDto>> Get(long bookId)
        {
            _crudRepository.Get<Book>(bookId);
            var reviews = _reviewsRepository
                .GetAll(bookId)
                .Select(review => _mapper.Map<ReviewOutputDto>(review));
            return Ok(reviews);
        } 

        [HttpGet("{bookId}/reviews/{reviewId}")]
        public ActionResult<ReviewOutputDto> Get(long bookId, long reviewId) 
        {
            _crudRepository.Get<Book>(bookId);
            var review = _reviewsRepository.Get(bookId, reviewId);
            var reviewDto = _mapper.Map<ReviewOutputDto>(review);
            return Ok(reviewDto);
        } 

        [HttpPost]
        [Route("{bookId}/reviews")]
        public ActionResult<ReviewOutputDto> Post(long bookId, [FromBody] ReviewInputDto reviewDto)
        {
            var review = _mapper.Map<ReviewInputDto, Review>(reviewDto);
            review.User = _crudRepository.Get<User>(reviewDto.UserId);
            review.Book = _crudRepository.Get<Book>(bookId);
            var insertedReview = _reviewsRepository.Insert(review);
            var insertedReviewDto = _mapper.Map<ReviewOutputDto>(insertedReview);
            return StatusCode(201, insertedReviewDto);
        }

        [HttpPut("{bookId}/reviews/{reviewId}")]
        public ActionResult<ReviewOutputDto> Put(long bookId, long reviewId, [FromBody] ReviewInputDto reviewDto)
        {
            _crudRepository.Get<Book>(bookId);
            var review = _reviewsRepository.Get(bookId, reviewId);
            review.Rate = reviewDto.Rate;
            review.Comment = reviewDto.Comment;
            var updatedReview = _reviewsRepository.Update(bookId, review);
            var updatedReviewDto = _mapper.Map<ReviewOutputDto>(review);
            return Ok(updatedReviewDto);
        }

        [HttpDelete("{bookId}/reviews/{reviewId}")]
        public IActionResult Delete(long bookId, long reviewId)
        {
            _crudRepository.Get<Book>(bookId);
            _reviewsRepository.Delete(reviewId);
            return NoContent();
        }
    }
}