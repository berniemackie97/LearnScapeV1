namespace Core.BusinessModels
{
    public class ProductBM : BaseEntityBM
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public ProductTypeBM ProductType { get; set; }

        public int ProductTypeId { get; set; }

        public ProductBrandBM ProductBrand { get; set; }

        public int ProductBrandId { get; set; }
    }
}
