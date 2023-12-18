using HotelListing.API.Core.Models;

namespace HotelListing.API.Core.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        public DateTime LastChanged { get; set; }
        public string LastChangedBy { get; set; }

        Task<T?> GetAsync(Guid? id);
        Task<TResult> GetAsync<TResult>(Guid? id);
        Task<List<T>> GetAllAsync();
        Task<List<TResult>> GetAllAsync<TResult>();
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
        Task<T> AddAsync(T entity);
        Task<TResult> AddAsync<TSource, TResult>(TSource source);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(T entity);
        Task UpdateAsync<TSource>(Guid id, TSource source) where TSource : IBaseDto;
        Task<bool> Exists(Guid id);

    }
}
