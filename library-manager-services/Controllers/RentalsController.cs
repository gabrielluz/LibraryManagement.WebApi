using System;
using System.Collections.Generic;
using LibraryManager.Exceptions;
using LibraryManager.Models.Entities;
using LibraryManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class RentalsController : ApiController
    {
        private readonly ICrudRepository _crudRepository;

        public RentalsController(ICrudRepository crudRepository) : base() 
        {
            _crudRepository = crudRepository;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_crudRepository.GetAll<Rental>());

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            try 
            {
                return Ok(_crudRepository.Get<Rental>(id));
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
            return Created(uriBuilder.Uri, _crudRepository.Insert(Rental));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Rental Rental)
        {
            try 
            {
                return Ok(_crudRepository.Update(id, Rental));
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
                _crudRepository.Delete<Rental>(id);
                return Ok();
            }
            catch (EntityNotFoundException<Rental> ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}