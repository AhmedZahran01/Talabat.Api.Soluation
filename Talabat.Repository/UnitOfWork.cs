using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core;
using Talabat.core.Entities;
using Talabat.core.Entities.OrderAggregates;
using Talabat.core.Repostories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _Repostories;  

        public UnitOfWork(StoreContext dbContext)
        {
            this._dbContext = dbContext;
            _Repostories = new Hashtable();

        }

        #region Comment Automatic Prop Region

        //public IGenericRepostory<Product> productsRepo { get; set; }
        //public IGenericRepostory<ProductCategory> categoriesRepo { get; set; }
        //public IGenericRepostory<DeliveryMethod> deliveryMethodsRepo { get; set; }
        //public IGenericRepostory<ProductBrand> BrandsRepo { get; set; }
        //public IGenericRepostory<OrderItem> orderitemsRepo { get; set; }
        //public IGenericRepostory<Order> ordersRepo { get; set; }

        #endregion


        public IGenericRepostory<TEntity> Repostory<TEntity>() where TEntity : BaseEntity
        { 
            var key = typeof(TEntity).Name;
            if(!_Repostories.ContainsKey(key))
            {
                var repostory = new GenericRepostory<TEntity>(_dbContext) ;
                _Repostories.Add(key, repostory);
            } 
            return _Repostories[key] as IGenericRepostory<TEntity>;
             
        }

        public async Task<int> CompleteAsync()
         => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
          => await _dbContext.DisposeAsync();

    }
}
