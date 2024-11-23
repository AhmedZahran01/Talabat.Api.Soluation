using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core;
using Talabat.core.Entities;
using Talabat.core.Services.Contract;
using Talabat.core.Specifications.Product_Specs;

namespace Talabat.Services
{
    public class ProductService : IProductService
    {
        #region Constractor Region

        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #endregion


        #region  GetCategoriesAsync Region

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        {
            var Brands = await _unitOfWork.Repostory<ProductCategory>().GetAllAsync();
            return Brands;

        }
        #endregion


        #region  GetBrandsAsync Region

        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            var Brands = await _unitOfWork.Repostory<ProductBrand>().GetAllAsync();
            return Brands;

        }

        #endregion


        #region Get All Products Region

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams);
            var products = await _unitOfWork.Repostory<Product>().GetAllWithSpecAsync(spec);

            return products;
           
        }

        #endregion

       
        #region GetCountAsync Region
      
        public async Task<int> GetCountAsync(ProductSpecParams specParams)
        {
            var CountSpec = new ProductWithFilterationForCountSpecifications(specParams);
            var countData = await _unitOfWork.Repostory<Product>().GetCountAsnc(CountSpec /*spec*/);
            return countData;
        }
        #endregion


        #region  GetProductAsync Region


        public async Task<Product?> GetProductAsync(int ProductId)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(ProductId);
            var product = await _unitOfWork.Repostory<Product>().GetEntityWithSpecAsync(spec);
            return product;
        }
        #endregion

    }
}
