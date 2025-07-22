using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //Task<IEnumerable<T>> GetAllAsync();
        //Task<T?> Get(Expression<Func<T, bool>> filter);
        //Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        //void Add(T entity);
        //void Update(T entity);
        //void Delete(T entity);
        //void DeleteRange(IEnumerable<T> entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
