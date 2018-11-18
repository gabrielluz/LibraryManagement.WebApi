using System;
using System.Collections.Generic;
using LibraryManager.Exceptions;
using LibraryManager.Models.Dto;
using LibraryManager.Models.Entities;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
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
            return Ok(_rentalRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] RentalOutputDto Rental)
        {
            return StatusCode(201, _rentalRepository.Insert(Rental));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RentalOutputDto Rental)
        {
            return Ok(_rentalRepository.Update(id, Rental));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _rentalRepository.Delete(id);
            return NoContent();
        }
    }
}