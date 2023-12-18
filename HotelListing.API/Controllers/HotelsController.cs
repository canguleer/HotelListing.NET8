using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.Contracts;
using AutoMapper;
using HotelListing.API.Core.Models.Hotel;
using HotelListing.API.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class HotelsController(IHotelsRepository hotelsRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await hotelsRepository.GetAllAsync<HotelDto>();
            return Ok(hotels);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<HotelDto>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
{
            var pagedHotelsResult = await hotelsRepository.GetAllAsync<HotelDto>(queryParameters);
            return Ok(pagedHotelsResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(Guid id)
        {
            var hotel = await hotelsRepository.GetAsync<HotelDto>(id);
            return Ok(hotel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(Guid id, HotelDto hotelDto)
        {
            try
            {
                await hotelsRepository.UpdateAsync(id, hotelDto);
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
            var hotel = await hotelsRepository.AddAsync<CreateHotelDto, HotelDto>(hotelDto);
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            await hotelsRepository.DeleteAsync(id); 
            return NoContent();
        }

        private async Task<bool> HotelExists(Guid id)
        {
            return await hotelsRepository.Exists(id);
        }
    }
}
