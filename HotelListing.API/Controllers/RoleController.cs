using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data.Entities;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [ApiVersion("1.0", Deprecated = true)]
    public class RoleController : ControllerBase
     {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleRepository roleRepository, IMapper mapper, ILogger<RoleController> logger)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        // GET: api/Role/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetRoles()
        {
            var role = await _roleRepository.GetAllAsync<Role>();
            return Ok(role);
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(Guid id)
        {
            var role = await _roleRepository.GetAsync(id);
            return Ok(role);
        }

        [HttpGet("RoleExists")]
        public async Task<bool> RoleExists(Guid id)
        {
            return await _roleRepository.Exists(id);
        }
    }
}
