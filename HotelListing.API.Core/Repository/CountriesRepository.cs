using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Core.Models.Country;
using HotelListing.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using HotelListing.API.Core.Models.Hotel;
using Microsoft.Extensions.Logging;

namespace HotelListing.API.Core.Repository
{
    public class CountriesRepository(HotelListingDbContext context, IMapper mapper, ILogger<Country> logger) : GenericRepository<Country>(context, mapper,logger), ICountriesRepository
    {
        public async Task<CountryDto> GetDetails(Guid id)
        {
            var country = await context.Country
                .Select(c => new CountryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ShortName = c.ShortName,
                    Hotels = c.Hotel.Select(h => new HotelDto
                    {
                        Id = h.Id,
                        Name = h.Name,
                        Address = h.Address,
                        Rating = h.Rating,
                        CountryId = h.CountryId,
                    }).ToList(),
                }).SingleOrDefaultAsync(q => q.Id == id);

            //var country = await context.Country
            //    .Include(q => q.Hotel)
            //    .ProjectTo<CountryDto>(mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync(q => q.Id == id);

            return country ?? throw new NotFoundException(nameof(GetDetails), id);
        }
    }
}
