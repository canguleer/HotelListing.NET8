using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.Contracts;
using AutoMapper;
using HotelListing.API.Core.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Core.Models;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 //   [Authorize]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository hotelsRepository, IMapper mapper)
        {
            this._hotelsRepository = hotelsRepository;
            this._mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _hotelsRepository.GetAllAsync<HotelDto>();
            return Ok(hotels);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<HotelDto>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
{
            var pagedHotelsResult = await _hotelsRepository.GetAllAsync<HotelDto>(queryParameters);
            return Ok(pagedHotelsResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync<HotelDto>(id);
            return Ok(hotel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDto hotelDto)
        {
            try
            {
                await _hotelsRepository.UpdateAsync(id, hotelDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id)) 
                    return NotFound();
                else throw; 
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<HotelDto>> PostHotel(CreateHotelDto hotelDto)
        {
            var hotel = await _hotelsRepository.AddAsync<CreateHotelDto, HotelDto>(hotelDto);
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotelsRepository.DeleteAsync(id); 
            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
