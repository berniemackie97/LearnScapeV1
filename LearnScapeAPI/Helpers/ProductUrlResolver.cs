using AutoMapper;
using Core.BusinessModels;
using LearnScapeAPI.DTO;

namespace LearnScapeAPI.Helpers
{
    public class ProductUrlResolver : IValueResolver<ProductBM, ProductToReturnDTO, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(ProductBM source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            Exception ex = new Exception("No Souce Found");

            return ex.Message;
        }
    }
}
