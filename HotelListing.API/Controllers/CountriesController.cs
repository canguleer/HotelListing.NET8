using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.Models.Country;
using AutoMapper;
using HotelListing.API.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Core.Models;

namespace HotelListing.API.Controllers
{
    [Route("api/v{version:apiVersion}/countries")]
    [ApiController]
    //[Authorize]
    [ApiVersion("1.0", Deprecated = true)]
    public class CountriesController : ControllerBase
     {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(ICountriesRepository countriesRepository, IMapper mapper, ILogger<CountriesController> logger)
        {
            _mapper = mapper;
            _countriesRepository = countriesRepository;
            _logger = logger;
        }

        // GET: api/Country/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countriesRepository.GetAllAsync<GetCountryDto>();
            return Ok(countries);
        }

        // GET: api/Country/?StartIndex=0&pagesize=25&PageNumber=1
        [HttpGet("GetPagedCountries")]
        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetPagedCountries([FromQuery] QueryParameters queryParameters)
        {
            var pagedCountriesResult = await _countriesRepository.GetAllAsync<GetCountryDto>(queryParameters);
            return Ok(pagedCountriesResult);
        }

        // GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(Guid id)
        {
            var country = await _countriesRepository.GetAsync(id);
            return Ok(country);
        }

        // GET: api/Country/5
        [HttpGet("GetCountryDetails/{id}")]
        public async Task<IActionResult> GetCountryDetails(Guid id)
        {
            var country = await _countriesRepository.GetDetails(id);
            return Ok(country);
        }

        // PUT: api/Country/5
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> PutCountry(Guid id, UpdateCountryDto updateCountryDto)
        {
            try
            {
                await _countriesRepository.UpdateAsync(id, updateCountryDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // POST: api/Country
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> PostCountry(CreateCountryDto createCountryDto)
        {
            var country = await _countriesRepository.AddAsync<CreateCountryDto, GetCountryDto>(createCountryDto);
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
        }

        // DELETE: api/Country/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CountryDto>> DeleteCountry(Guid id)
        {
            var county = await _countriesRepository.GetAsync(id);
            if (county == null)
                return NotFound();

            await _countriesRepository.DeleteAsync(id);
            return NoContent();
        }


        [HttpGet("CountryExists")]
        public async Task<bool> CountryExists(Guid id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
