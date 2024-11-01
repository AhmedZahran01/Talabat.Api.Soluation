using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext dbcontext)
        {
            //if (!dbcontext.productBrands.Any())
           if (dbcontext.productBrands.Count() == 0)
            {


                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
                var brnds = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                //if (brnds is not null && brnds.Count>0)
                //{

                if (brnds?.Count() > 0)
                {
                    foreach (var Brand in brnds)
                    {
                        dbcontext.Set<ProductBrand>().Add(Brand);
                    }
                    await dbcontext.SaveChangesAsync();

                } 
            }

            if (dbcontext.productCategories.Count() == 0)
            { 
                var CategoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
                var category = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);

                //if (brnds is not null && brnds.Count>0)
                //{ 
                if (category?.Count() > 0)
                {
                    foreach (var caategory in category)
                    {
                        dbcontext.Set<ProductCategory>().Add(caategory);
                    }
                    await dbcontext.SaveChangesAsync(); 
                }
            }

            if (dbcontext.Products.Count() == 0)
            {
                var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                //if (brnds is not null && brnds.Count>0)
                //{ 
                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        dbcontext.Set<Product>().Add(product);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

        }


    }
}
