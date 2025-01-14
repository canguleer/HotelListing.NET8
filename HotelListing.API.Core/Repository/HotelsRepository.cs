﻿using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data.Entities;
using Microsoft.Extensions.Logging;

namespace HotelListing.API.Core.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelsRepository(HotelListingDbContext context, IMapper mapper, ILogger<Hotel> logger) : base(context, mapper,logger)
        {
        }
    }
}
