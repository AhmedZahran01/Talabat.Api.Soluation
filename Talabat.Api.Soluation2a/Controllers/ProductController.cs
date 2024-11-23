using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Soluation2a.DTOs;
using Talabat.Api.Soluation2a.Errors;
using Talabat.Api.Soluation2a.Helpers;
using Talabat.core.Entities;
using Talabat.core.Repostories.Contract;
using Talabat.core.Services.Contract;
using Talabat.core.Specifications;
using Talabat.core.Specifications.Product_Specs;

namespace Talabat.Api.Soluation2a.Controllers
{

    public class ProductController : BaseApiController
    { 

        #region Constractor Region

        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        ///private readonly IGenericRepostory<Product> _productsRepo;
        ///private readonly IGenericRepostory<ProductCategory> _categoryRepo;
        ///private readonly IGenericRepostory<ProductBrand> _brandRepo;

        public ProductController(
             IMapper mapper, IProductService productService
             ///IGenericRepostory<Product> productsRepo, IGenericRepostory<ProductCategory> CategoryRepo,
             ///IGenericRepostory<ProductBrand> BrandRepo
            )
        {
            _mapper = mapper;
            this._productService = productService;
            ///_productsRepo = productsRepo;
            ///_categoryRepo = CategoryRepo;
            ///_brandRepo = BrandRepo;
        }
        #endregion

        #region Comment Mvc Controller

        //[HttpGet]
        //public async Task<IActionResult> GetProducts()
        //{
        //    var products = await _productsRepo.GetAllAsync();

        //    return Ok(products);

        //} 
        #endregion

        #region Get All Products Region
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize]
        [HttpGet("Get All Products")]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
             var products = await _productService.GetProductsAsync(specParams);
             var countData = await  _productService.GetCountAsync(specParams);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex , specParams.PageSize, countData, data));
        }  
             
        #endregion
          
        #region Product by id

        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiRespone), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {  
            var product = await _productService.GetProductAsync(id);
            //var x = product.ToString();

            var mappervalueProduct = _mapper.Map<Product, ProductToReturnDto>(product);

            if (product is null) return NotFound(new ApiRespone(404));

            return Ok(mappervalueProduct);
        }

        #endregion
        
        #region brands Region

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands = await  _productService.GetBrandsAsync();
            return Ok(Brands);
        }
        #endregion
          
        #region  Categories Region

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllcategory()
        {
            var categories = await _productService.GetCategoriesAsync();
            return Ok(categories);
        } 
        #endregion
         
    }
}
