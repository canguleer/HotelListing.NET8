﻿using HotelListing.API.Core.Models.Country;
using HotelListing.API.Data.Entities;

namespace HotelListing.API.Core.Contracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        Task<CountryDto> GetDetails(Guid id);
    }
}
