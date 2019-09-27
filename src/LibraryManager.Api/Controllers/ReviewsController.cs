using AutoMapper;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryManager.Api.Controllers
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
            var reviews = _reviewsRepository.GetAll(bookId);
            var reviewsOutputDto = _mapper.Map<IEnumerable<ReviewOutputDto>>(reviews);
            return Ok(reviewsOutputDto);
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
        public ActionResult<ReviewOutputDto> Put(long bookId, long reviewId, [FromBody] UpdateReviewDto reviewDto)
        {
            var review = _reviewsRepository.Get(bookId, reviewId);

            review.Rate = reviewDto.Rate;
            review.Comment = reviewDto.Comment;

            var updatedReview = _reviewsRepository.Update(bookId, review);
            var updatedReviewDto = _mapper.Map<ReviewOutputDto>(updatedReview);

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