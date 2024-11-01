using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repostories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepostory<T> : IGenericRepostory<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepostory(StoreContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Product))
                 return (IReadOnlyList<T>) await _dbContext.Set<Product>().Include(p => p.category)
                                 .Include( p => p.Brand).ToListAsync();
           return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
                return await  _dbContext.Set<Product>().Where(p => p.Id == id).Include(p => p.category)
                                .Include(p => p.Brand).FirstOrDefaultAsync() as T;
            return await _dbContext.Set<T>().FindAsync(id); 

        }
    }
}
