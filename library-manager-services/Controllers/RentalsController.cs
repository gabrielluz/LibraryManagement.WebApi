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
    public class RentalsController : ControllerBase
    {
        private readonly Repository<Rental> _rentalsRepository;

        public RentalsController() : base() 
        {
            _rentalsRepository = new Repository<Rental>();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_rentalsRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            try 
            {
                return Ok(_rentalsRepository.Get(id));
            }
            catch (EntityNotFoundException<Rental> ex)
            {
                return NotFound(ex.Message);
            }
        } 

        [HttpPost]
        public IActionResult Post([FromBody] Rental Rental)
        {
            var uriBuilder = new UriBuilder()
            {
                Host = this.HttpContext.Request.Host.Host
            };
            return Created(uriBuilder.Uri, _rentalsRepository.Insert(Rental));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Rental Rental)
        {
            try 
            {
                return Ok(_rentalsRepository.Update(id, Rental));
            }
            catch(EntityNotFoundException<Rental> ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try 
            {
                _rentalsRepository.Delete(id);
                return Ok();
            }
            catch (EntityNotFoundException<Rental> ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}