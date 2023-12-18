using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Data.Entities;
using Microsoft.Extensions.Logging;

namespace HotelListing.API.Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<T> _logger;

        public GenericRepository(HotelListingDbContext context, IMapper mapper, ILogger<T> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
        {
            var entity = _mapper.Map<T>(source);
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went wrong while processing {nameof(AddAsync)} on {typeof(T)} entity ");
            }
            
            return _mapper.Map<TResult>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);

            if (entity is null)
                throw new NotFoundException(typeof(T).Name, id);
            

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
        {
            var totalSize = await _context.Set<T>().CountAsync();
            var items = await _context.Set<T>()
                .Skip(queryParameters.StartIndex)
                .Take(queryParameters.PageSize)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<TResult>
            {
                Items = items,
                PageNumber = queryParameters.PageNumber,
                RecordNumber = queryParameters.PageSize,
                TotalCount = totalSize
            };
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            return await _context.Set<T>()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<T?> GetAsync(Guid? id)
        {
            if (id is null) 
                return new T();

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<TResult> GetAsync<TResult>(Guid? id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result is null ? throw new NotFoundException(typeof(T).Name, id.HasValue ? id : "No Key Provided")
                : _mapper.Map<TResult>(result);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<TSource>(Guid id, TSource source) where TSource : IBaseDto
        {
            if (id != source.Id)
                throw new BadRequestException("Invalid Id used in request");

            var entity = await GetAsync(id);

            if (entity == null)
                throw new NotFoundException(typeof(T).Name, id);

            _mapper.Map(source, entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
