using AutoMapper;
using Core.BusinessModels;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using LearnScapeAPI.DTO;
using LearnScapeAPI.Errors;
using LearnScapeAPI.Helpers;
using LearnScapeCore.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnScapeAPI.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepo<ProductBM> _productRepo;
        private readonly IGenericRepo<ProductBrandBM> _productBrandRepo;
        private readonly IGenericRepo<ProductTypeBM> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepo<ProductBM> productRepo, IGenericRepo<ProductBrandBM> productBrandRepo, IGenericRepo<ProductTypeBM> productTypeRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationHelper<ProductToReturnDTO>>> GetProducts([FromQuery]ProductParameterSpecifications productParameter)
        {
            ProductsWithTypesAndBrandsSpecification spec = new ProductsWithTypesAndBrandsSpecification(productParameter);

            ProductWithFiltersForCountSpecification countSpec = new ProductWithFiltersForCountSpecification(productParameter);

            int totalItems = await _productRepo.CountAsync(countSpec);

            IReadOnlyList<ProductBM> products = await _productRepo.ListAsync(spec);
            
            IReadOnlyList<ProductToReturnDTO> data = _mapper.Map<IReadOnlyList<ProductBM>, IReadOnlyList<ProductToReturnDTO>>(products);

            PaginationHelper<ProductToReturnDTO> filteredProducts = new PaginationHelper<ProductToReturnDTO>(productParameter.PageIndex, productParameter.PageSize, totalItems, data);
            
            return Ok(filteredProducts);

        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            ProductsWithTypesAndBrandsSpecification spec = new ProductsWithTypesAndBrandsSpecification(id);
            
            ProductBM product = await _productRepo.GetEntityWithSpec(spec);

            ProductToReturnDTO productDTO = _mapper.Map<ProductBM, ProductToReturnDTO>(product);

            if (productDTO == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(productDTO);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrandBM>>> GetProductBrands()
        {
            IReadOnlyList<ProductBrandBM> brands = await _productBrandRepo.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductTypeBM>>> GetProductTypes()
        {
            IReadOnlyList<ProductTypeBM> types = await _productTypeRepo.ListAllAsync();
            return Ok(types);
        }
    }
}
