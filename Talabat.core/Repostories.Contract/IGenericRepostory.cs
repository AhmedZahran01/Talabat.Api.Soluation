using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Specifications;

namespace Talabat.core.Repostories.Contract
{
    public interface IGenericRepostory<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T?> GetEntityWithSpecAsync(ISpecifications<T> spec);

        Task<int> GetCountAsnc(ISpecifications<T> spec);


        Task AddAsync (T entity);
        void Update   (T entity);
        void Delete   (T entity);
    }
}
