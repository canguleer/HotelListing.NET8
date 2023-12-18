using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data.Entities;
using Microsoft.Extensions.Logging;

namespace HotelListing.API.Core.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(HotelListingDbContext context, IMapper mapper, ILogger<User> logger) : base(context, mapper,logger)
        {

        }
    }
}
