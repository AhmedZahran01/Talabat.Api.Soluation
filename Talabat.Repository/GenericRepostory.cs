using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repostories.Contract;
using Talabat.core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepostory<T> : IGenericRepostory<T> where T : BaseEntity
    {
      
        #region Constractor Region
        
        private readonly StoreContext _dbContext;

        public GenericRepostory(StoreContext dbContext)
        {
            _dbContext = dbContext;
        } 
        
        #endregion

        #region Not Used Spec DP Region

        public async Task<T?> GetByIdAsync(int id)
        {
            //if (typeof(T) == typeof(Product))
            //    return await  _dbContext.Set<Product>().Where(p => p.Id == id).Include(p => p.category)
            //                    .Include(p => p.Brand).FirstOrDefaultAsync() as T;

            return await _dbContext.Set<T>().FindAsync(id);

        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if(typeof(T) == typeof(Product))
            //     return (IReadOnlyList<T>) await _dbContext.Set<Product>().Include(p => p.category)
            //                     .Include( p => p.Brand).ToListAsync();


            return await _dbContext.Set<T>().ToListAsync();
        }


        #endregion

         
        #region MyRegion

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsnc(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        #endregion

        #region Add ... Region

        public async Task AddAsync(T entity)
         => await _dbContext.AddAsync(entity);

        public void Update(T entity)
         => _dbContext.Update(entity);


        public void Delete(T entity)
         => _dbContext.Remove(entity);

        #endregion
  
    }

}
