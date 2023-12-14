using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Core.Models.Country;
using HotelListing.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository
{
    public class CountriesRepository (HotelListingDbContext context, IMapper mapper) : GenericRepository<Country>(context, mapper), ICountriesRepository
    {
        public async Task<CountryDto> GetDetails(Guid id)
        {
            var country = await context.Countries
                .Include(q => q.Hotels)
                .ProjectTo<CountryDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            return country ?? throw new NotFoundException(nameof(GetDetails), id);
        }
    }
}
