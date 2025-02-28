using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services
{
    public interface IDataService<T> where T : DomainObject
    {
        public Task<T> Create(T entity);
        
        public Task<T> GetById(int id);

        public Task<IEnumerable<T>> GetAll();

        public Task<T> Update(int id, T entity);
        
        public Task<bool> Delete(int id);

    }
}
