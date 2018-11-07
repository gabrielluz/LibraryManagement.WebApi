using System;
using System.Collections.Generic;
using LibraryManager.Exceptions;
using LibraryManager.Models.Dto;
using LibraryManager.Models.Entities;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class RentalsController : ApiController
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalsController(IRentalRepository rentalRepository) : base() 
        {
            _rentalRepository = rentalRepository;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_rentalRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            try 
            {
                return Ok(_rentalRepository.Get(id));
            }
            catch (EntityNotFoundException<Rental> ex)
            {
                return NotFound(new { ex.Message });
            }
        } 

        [HttpPost]
        public IActionResult Post([FromBody] RentalDto Rental)
        {
            var uriBuilder = new UriBuilder()
            {
                Host = this.HttpContext.Request.Host.Host
            };
            return Created(uriBuilder.Uri, _rentalRepository.Insert(Rental));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RentalDto Rental)
        {
            try 
            {
                return Ok(_rentalRepository.Update(id, Rental));
            }
            catch(EntityNotFoundException<Rental> ex)
            {
                return NotFound(new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try 
            {
                _rentalRepository.Delete(id);
                return Ok();
            }
            catch (EntityNotFoundException<Rental> ex)
            {
                return NotFound(new { ex.Message });
            }
        }
    }
}