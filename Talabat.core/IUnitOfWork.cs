using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Entities.OrderAggregates;
using Talabat.core.Repostories.Contract;

namespace Talabat.core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        #region MyRegion

        //public IGenericRepostory<Product> productsRepo { get; set; }
        //public IGenericRepostory<ProductCategory> categoriesRepo { get; set; }
        //public IGenericRepostory<DeliveryMethod> deliveryMethodsRepo { get; set; }
        //public IGenericRepostory<ProductBrand> BrandsRepo { get; set; }
        //public IGenericRepostory<OrderItem> orderitemsRepo { get; set; }
        //public IGenericRepostory<Order> ordersRepo { get; set; }

        #endregion

        IGenericRepostory<TEntity> Repostory<TEntity>() where TEntity : BaseEntity;

        Task<int> CompleteAsync();

    }
}
