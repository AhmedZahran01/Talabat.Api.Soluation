using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
       
        #region Constractor Region

      
        //for object to get all product . not criteria  
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams) :
           base(p => 
                          (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search ) ) &&
                          ( !specParams.BrandId.HasValue || p.BrandId == specParams.BrandId) &&
                          (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId) 
           )
        {
            IncludeMethods();
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        //OrderBy = p => p.Price;
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        //OrderByDesc = p => p.Price; 
                        AddOrderByDesc(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);

                        break;
                }
            }

            else
            {
                AddOrderBy(p => p.Name);
            }

            int take = specParams.PageSize ;
            int skip = (specParams.PageIndex -1)*specParams.PageSize;
            ApplyPagination(skip, take);

        }

        //for object to get specific product by it is id  and criteria like where condition 
        public ProductWithBrandAndCategorySpecifications(int id) : base(p => p.Id == id)
        {
            IncludeMethods();
        }

        #endregion

        #region Methods Region

        private void IncludeMethods()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.category);
        }



        #endregion



    }
}
