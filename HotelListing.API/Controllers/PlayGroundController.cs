using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayGroundController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<PlayGroundController> _logger;

        public PlayGroundController(IRoleRepository roleRepository, IMapper mapper, ILogger<PlayGroundController> logger)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("Test")]
        public Task<OkResult> Test()
        {
            var user = HttpContext.GetClaimsUser();

            return Task.FromResult(Ok());
        }
    }
}
