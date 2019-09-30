using AutoMapper;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryManager.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public ActionResult<PaginatedOutput<RentalOutputDto>> Get([FromQuery] PaginationFilter paginationFilter)
        {
            var rentalList = _rentalRepository.GetAllPaginated(paginationFilter ?? new PaginationFilter());
            var rentalOutputDtoList = _mapper.Map<IEnumerable<RentalOutputDto>>(rentalList);
            var paginatedResult = new PaginatedOutput<RentalOutputDto>(paginationFilter, rentalOutputDtoList);
            return Ok(paginatedResult);
        }

        [HttpGet("{id}")]
        public ActionResult<RentalOutputDto> Get(long id)
        {
            var rental = _rentalRepository.Get(id);
            var rentalDto = _mapper.Map<RentalOutputDto>(rental);
            return Ok(rentalDto);
        }

        [HttpPost]
        public ActionResult<RentalOutputDto> Post([FromBody] AddRentalInputDto dto)
        {
            var rental = _mapper.Map<Rental>(dto);
            var insertedRental = _rentalRepository.Insert(rental);
            var insertedRentalDto = _mapper.Map<RentalOutputDto>(insertedRental);
            return StatusCode(201, insertedRentalDto);
        }

        [HttpPut("{id}")]
        public ActionResult<RentalOutputDto> Put(long id, [FromBody] UpdateRentalInputDto updateRentalInputDto)
        {
            var rental = _rentalRepository.Get(id);
            rental.Returned = updateRentalInputDto.Returned;
            var updatedRental = _rentalRepository.Update(rental);
            var updatedRentalDto = _mapper.Map<RentalOutputDto>(updatedRental);
            return Ok(updatedRentalDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _rentalRepository.Delete(id);
            return NoContent();
        }
    }
}