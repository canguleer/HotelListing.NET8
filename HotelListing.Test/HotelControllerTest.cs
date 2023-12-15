using AutoMapper;
using HotelListing.API.Controllers;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Models.Hotel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelListing.Test
{
    public class HotelControllerTest
    {
        private readonly Mock<IHotelsRepository> _mockRepo;
        private readonly HotelsController _controller;
        private readonly IMapper _mapper;
        private List<HotelDto> hotels;
        public HotelControllerTest()
        {
            _mockRepo = new Mock<IHotelsRepository>();
            _controller = new HotelsController(_mockRepo.Object, _mapper);
            hotels = new List<HotelDto>()
            {
               new HotelDto()
               {
                   Id = new Guid(),
                   Name = "Sandals Resort and Spa",
                   Address = "Negril",
                   Rating = 4.5,
                   CountryId =new Guid()
               },
                 new HotelDto()
               {
                   Id = new Guid(),
                   Name = "Comfort Suites",
                   Address = "George Town",
                   Rating = 4.3,
                   CountryId =new Guid()
               },
                 new HotelDto()
               {
                   Id = new Guid(),
                   Name = "Grand Palldium",
                   Address = "Nassua",
                   Rating = 4,
                   CountryId =new Guid()
               }
            };

        }
        //MethodName/What methods can do / what
        [Fact]
        public async void GetHotel_ActionExecutes_ReturnOkResultWithHotel()
        {
            _mockRepo.Setup(x => x.GetAllAsync<HotelDto>()).ReturnsAsync(hotels);

            var result = await _controller.GetHotels();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnHotels = Assert.IsAssignableFrom<List<HotelDto>>(okResult.Value);
           
            Assert.Equal<int>(3, returnHotels.ToList().Count);
        }
    }
}
