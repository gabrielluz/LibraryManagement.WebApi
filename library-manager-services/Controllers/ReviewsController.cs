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
    public class ReviewsController : ControllerBase
    {
        private readonly Repository<Review> _reviewsRepository;

        public ReviewsController() : base() 
        {
            _reviewsRepository = new Repository<Review>();
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
                return NotFound(ex.Message);
            }
        } 

        [HttpPost]
        public IActionResult Post([FromBody] Review Review)
        {
            var uriBuilder = new UriBuilder()
            {
                Host = this.HttpContext.Request.Host.Host
            };
            return Created(uriBuilder.Uri, _reviewsRepository.Insert(Review));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review Review)
        {
            try 
            {
                return Ok(_reviewsRepository.Update(id, Review));
            }
            catch(EntityNotFoundException<Review> ex)
            {
                return NotFound(ex.Message);
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
                return NotFound(ex.Message);
            }
        }
    }
}