using AutoMapper;
using HotelListing.API.Controllers;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Core.Models.Country;
using HotelListing.API.Core.Models.Hotel;
using HotelListing.API.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using HotelListing.API.Data.Entities;

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
        public async Task GetCountry_InValid_ReturnNotFound(int countryId)
        {
            _mockRepo.Setup(x => x.GetDetails(countryId)).Throws(new Exception($"GetCountry with id ({countryId}) was not found"));

            Exception ex = await Assert.ThrowsAsync<Exception>(() => _controller.GetCountry(countryId));
            Assert.Equal($"GetCountry with id ({countryId}) was not found", ex.Message);
            Assert.Null(ex.InnerException);
        }


        [Theory]
        [InlineData(1)]
        public async Task PutCountry_IdIsNotEqualCountry_ReturnBadRequestException(int countryId)
        {
            var data = getCountries.Select(x => new UpdateCountryDto()
            {
                Id = x.Id,
                Name = x.Name,
                ShortName = x.ShortName
            }).First(x => x.Id == countryId);

            _mockRepo.Setup(x => x.UpdateAsync(countryId, data)).ThrowsAsync(new Exception("Invalid Id used in request"));

            Exception ex = await Assert.ThrowsAsync<Exception>(() => _controller.PutCountry(countryId, data));
            Assert.Equal("Invalid Id used in request", ex.Message);
            Assert.Null(ex.InnerException);
        }


        //This method is not working correctly. Check 
        [Theory]
        [InlineData(1)]
        public async Task PutCountry_EntityIsNull_ReturnNotFoundException(int countryId)
        {
            var data = getCountries.Select(x => new UpdateCountryDto()
            {
                Id = x.Id,
                Name = x.Name,
                ShortName = x.ShortName
            }).FirstOrDefault(x => x.Id == countryId);


            Country country = getCountries.Select(x => new Country() { Id = x.Id, Name = x.Name, ShortName = x.ShortName })
                .FirstOrDefault(x => x.Id == countryId);


            _mockRepo.Setup(x => x.GetAsync(countryId)).ReturnsAsync(country);

            _mockRepo.Setup(x => x.GetAsync(countryId)).ThrowsAsync(new Exception($"Country with id ({countryId}) was not found"));

            Exception ex = await Assert.ThrowsAsync<Exception>(() => _controller.PutCountry(countryId, data));

            Assert.Equal($"Country with id ({countryId}) was not found", ex.Message);
            Assert.Null(ex.InnerException);

        }

        [Theory]
        [InlineData(1)]
        public async Task PutCountry_ActionExecutes_ReturnNoContentAsync(int countryId)
        {
            var data = getCountries.First(x => x.Id == countryId);

            var updateCountryDto = getCountries.Select(x => new UpdateCountryDto()
            {
                Id = x.Id,
                Name = x.Name,
                ShortName = x.ShortName
            }).First(x => x.Id == countryId);

            _mockRepo.Setup(x => x.UpdateAsync(countryId, updateCountryDto));

            var result = await _controller.PutCountry(countryId, updateCountryDto);

            _mockRepo.Verify(x => x.UpdateAsync(countryId, updateCountryDto), Times.Once);

            var returnNoContent = Assert.IsType<NoContentResult>(result);

            Assert.Equal<int>(204, returnNoContent.StatusCode);

        }

        [Theory]
        [InlineData(1)]
        public async Task PutCountry_IfCountryIsNotExists_ReturnNotFound(int countryId)
        {
            var data = getCountries.First(x => x.Id == countryId);

            var updateCountryDto = getCountries.Select(x => new UpdateCountryDto()
            {
                Id = x.Id,
                Name = x.Name,
                ShortName = x.ShortName
            }).First(x => x.Id == countryId);

            _mockRepo.Setup(x => x.UpdateAsync(countryId, updateCountryDto));
            _mockRepo.Setup(x => x.Exists(10));

            var result = await _controller.PutCountry(countryId, updateCountryDto);
            //var result2 = await _controller.CountryExists(10);

            _mockRepo.Verify(x => x.UpdateAsync(countryId, updateCountryDto), Times.Once);

            var returnNotFound = Assert.IsType<NotFoundResult>(result);

            Assert.Equal<int>(404, returnNotFound.StatusCode);

        }

        [Fact]
        public async void PostCountry_ActionExecutes_ReturnCreatedAction()
        {
            var country = getCountries.
                Select(x => new Country() { Id = x.Id, Name = x.Name, ShortName = x.ShortName }).First();

            var getCountry = getCountries.
               Select(x => new GetCountryDto() { Id = x.Id, Name = x.Name, ShortName = x.ShortName }).First();

            var createCountyDto = getCountries
                .Select(x => new CreateCountryDto() { Name = x.Name, ShortName = x.ShortName })
                .First();

            _mockRepo.Setup(x => x.AddAsync<CreateCountryDto, GetCountryDto>(createCountyDto))
                  .Returns((Task<GetCountryDto>)Task.CompletedTask);

            var resut = await _controller.PostCountry(createCountyDto);

            var createdActionResult = Assert.IsType<CreatedAtActionResult>(resut);

            _mockRepo.Verify(_mockRepo => _mockRepo.AddAsync(country), Times.Once);

            Assert.Equal("GetCountry", createdActionResult.ActionName);
        }

        [Theory]
        [InlineData(0)]
        public async Task DeleteCountry_IdInValid_ReturnNotFound(int countryId)
        {
            Country country = null;

            _mockRepo.Setup(x => x.GetAsync(countryId)).ReturnsAsync(country);

            var resultNotFound = await _controller.DeleteCountry(countryId);

            //if I return IactionResuklt, it is enough to write just resultNotFound, but if I return actionresult and class then I must also write ".Result".
            Assert.IsType<NotFoundResult>(resultNotFound.Result);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteCountry_ActionExecutes_ReturnNoContent(int countryId)
        {
            CountryDto countryDto = getCountriesWithHotels.First(x => x.Id == countryId);
            Country country = getCountries.Select(x => new Country() { Id = x.Id, Name = x.Name, ShortName = x.ShortName })
                .First(x => x.Id == countryId);

            _mockRepo.Setup(x => x.GetAsync(countryId)).ReturnsAsync(country);

            _mockRepo.Setup(x => x.DeleteAsync(countryId));

            var noContentResult = await _controller.DeleteCountry(countryId);

            _mockRepo.Verify(x => x.DeleteAsync(countryId), Times.Once);

            Assert.IsType<NoContentResult>(noContentResult.Result);


        }

    }
}
