using Core.BusinessModels;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    string brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    List<ProductBrandBM> brands = JsonSerializer.Deserialize<List<ProductBrandBM>>(brandsData);

                    foreach (ProductBrandBM item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    string brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    List<ProductTypeBM> types = JsonSerializer.Deserialize<List<ProductTypeBM>>(brandsData);

                    foreach (ProductTypeBM item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    string brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    List<ProductBM> products = JsonSerializer.Deserialize<List<ProductBM>>(brandsData);

                    foreach (ProductBM item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                ILogger<StoreContextSeed> logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
