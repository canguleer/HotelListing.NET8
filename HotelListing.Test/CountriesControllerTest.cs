using AutoMapper;
using HotelListing.API.Controllers;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Core.Models.Country;
using HotelListing.API.Core.Models.Hotel;
using HotelListing.API.Core.Repository;
using HotelListing.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelListing.Test
{

    public class CountriesControllerTest
    {
        private readonly Mock<ICountriesRepository> _mockRepo;
        private readonly CountriesController _controller;
        private readonly IMapper _mapper;
        private readonly ILogger<CountriesController> _logger;


        private List<GetCountryDto> getCountries;
        private List<CountryDto> getCountriesWithHotels;
        private List<HotelDto> hotels;
        public CountriesControllerTest()
        {
            _mockRepo = new Mock<ICountriesRepository>();
            _controller = new CountriesController(_mockRepo.Object, _mapper, _logger);
            getCountries = new List<GetCountryDto>() {
                new GetCountryDto() { Id = 1, Name = "Turkey", ShortName = "TR" },
                new GetCountryDto() { Id = 1, Name = "Bulgaria", ShortName = "BG" },
                new GetCountryDto() { Id = 1, Name = "Germany", ShortName = "GN" }
            };

            hotels = new List<HotelDto>()
            {
               new HotelDto()
               {
                   Id = 1,
                   Name = "Sandals Resort and Spa",
                   Address = "Negril",
                   Rating = 4.5,
                   CountryId = 1
               },
                 new HotelDto()
               {
                   Id = 2,
                   Name = "Comfort Suites",
                   Address = "George Town",
                   Rating = 4.3,
                   CountryId = 2
               },
                 new HotelDto()
               {
                   Id = 3,
                   Name = "Grand Palldium",
                   Address = "Nassua",
                   Rating = 4,
                   CountryId = 3
               }
            };

            getCountriesWithHotels = new List<CountryDto>() {
                new CountryDto() { Id = 1, Name = "Turkey", ShortName = "TR",Hotels = hotels},
                new CountryDto() { Id = 1, Name = "Bulgaria", ShortName = "BG",Hotels = hotels },
                new CountryDto() { Id = 1, Name = "Germany", ShortName = "GN",Hotels = hotels }
            };
        }


        //MethodName/What methods can do / what
        [Fact]
        public async void GetCountries_ActionExecutes_ReturnOkResultWithCountry()
        {
            _mockRepo.Setup(x => x.GetAllAsync<GetCountryDto>()).ReturnsAsync(getCountries);

            var result = await _controller.GetCountries();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnCountries = Assert.IsAssignableFrom<IEnumerable<GetCountryDto>>(okResult.Value);

            Assert.Equal<int>(3, returnCountries.ToList().Count);
        }

        [Theory]
        [InlineData(1)]
        public async void GetCountry_ActionExecutes_ReturnOkResultWithCountry(int countryId)
        {
            CountryDto country = getCountriesWithHotels.First(x => x.Id == countryId);

            _mockRepo.Setup(x => x.GetDetails(countryId)).ReturnsAsync(country);

            var result = await _controller.GetCountry(countryId);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnCountry = Assert.IsAssignableFrom<CountryDto>(okResult.Value);

            Assert.Equal<int>(countryId, returnCountry.Id);
        }

        [Theory]
        [InlineData(0)]
        public async Task GetCountryTest_InValid_ReturnNotFound(int countryId)
        {
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _controller.GetCountryTest(countryId));
            Assert.Equal($"GetCountryTest with id ({countryId}) was not found", ex.Message);
            Assert.Null(ex.InnerException);
        }

        [Theory]
        [InlineData(1)]
        public async Task PutCountry_IdIsNotEqualCountry_BadRequestResultAsync(int countryId)
        {
            var data = getCountries.Select(x => new UpdateCountryDto()
            {
                Id = x.Id,
                Name = x.Name,
                ShortName = x.ShortName
            }).First(x => x.Id == countryId);

            var result =  await _controller.PutCountry(2, data);
            Assert.NotEqual<int>(2, countryId);
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal<int>(400, badRequestResult.StatusCode);
        }
    }
}
