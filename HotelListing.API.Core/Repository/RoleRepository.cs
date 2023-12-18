using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data.Entities;
using Microsoft.Extensions.Logging;

namespace HotelListing.API.Core.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(HotelListingDbContext context, IMapper mapper, ILogger<Role> logger) : base(context, mapper, logger)
        {

        }
    }
}
