using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Soluation2a.DTOs;
using Talabat.Api.Soluation2a.Errors;
using Talabat.core.Entities;
using Talabat.core.Repostories.Contract;
using Talabat.core.Specifications;
using Talabat.core.Specifications.Product_Specs;

namespace Talabat.Api.Soluation2a.Controllers
{
     
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepostory<Product> _productsRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepostory<ProductCategory> _categoryRepo;
        private readonly IGenericRepostory<ProductBrand> _brandRepo;

        public ProductController(IGenericRepostory<Product> productsRepo, IMapper mapper ,
                                 IGenericRepostory<ProductCategory> CategoryRepo , IGenericRepostory<ProductBrand> BrandRepo)
        {
           _productsRepo = productsRepo;
           _mapper = mapper;
           _categoryRepo = CategoryRepo;
           _brandRepo = BrandRepo;
        }

        #region Mvc Controller

        //[HttpGet]
        //public async Task<IActionResult> GetProducts()
        //{
        //    var products = await _productsRepo.GetAllAsync();

        //    return Ok(products);

        //} 
        #endregion

        [HttpGet("Get All Products")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts( [FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams);
            var products = await _productsRepo.GetAllWithSpecAsync(spec);
            var mappervalueProduct = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(mappervalueProduct); 
        }
         

        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiRespone), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _productsRepo.GetWithSpecAsync(spec);
            //var x = product.ToString();

            var mappervalueProduct = _mapper.Map<Product, ProductToReturnDto>(product);
             
            if (product is null) return NotFound(new ApiRespone(404));

            return Ok(mappervalueProduct);
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _brandRepo.GetAllAsync();
            return Ok(Brands);
        }


        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllcategory()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }


    }
}
