using System;
using System.Collections.Generic;
using AutoMapper;
using LibraryManagerApi.Exceptions;
using LibraryManagerApi.Models.Dto;
using LibraryManagerApi.Models.Entities;
using LibraryManagerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public RentalsController(
            IRentalRepository rentalRepository,
            IMapper mapper) : base() 
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rental>> Get() => Ok(_rentalRepository.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Rental> Get(long id) 
        {
            return Ok(_rentalRepository.Get(id));
        }

        [HttpPost]
        public ActionResult<Rental> Post([FromBody] RentalInputDto dto)
        {
            var entity = _mapper.Map<Rental>(dto);
            return StatusCode(201, _rentalRepository.Insert(entity));
        }

        [HttpPut("{id}")]
        public ActionResult<Rental> Put(long id, [FromBody] RentalInputDto dto)
        {
            var entity = _rentalRepository.Get(id);
            return Ok(_rentalRepository.Update(entity));
        }

        [HttpDelete("{id}")]
        public ActionResult<Rental> Delete(long id)
        {
            _rentalRepository.Delete(id);
            return NoContent();
        }
    }
}