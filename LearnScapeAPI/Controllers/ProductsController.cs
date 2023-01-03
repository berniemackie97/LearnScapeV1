using AutoMapper;
using Core.BusinessModels;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using LearnScapeAPI.DTO;
using LearnScapeAPI.Errors;
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);

            IReadOnlyList<ProductToReturnDTO> productDTO = _mapper.Map<IReadOnlyList<ProductBM>, IReadOnlyList<ProductToReturnDTO>>(products);

            return Ok(productDTO);

        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            
            var product = await _productRepo.GetEntityWithSpec(spec);

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
            var brands = await _productBrandRepo.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductTypeBM>>> GetProductTypes()
        {
            var types = await _productTypeRepo.ListAllAsync();
            return Ok(types);
        }
    }
}
