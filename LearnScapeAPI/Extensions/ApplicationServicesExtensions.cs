using Core.Interfaces;
using Infrastructure.Data;
using LearnScapeAPI.Errors;
using LearnScapeCore.Interfaces;
using LearnScapeInfrastructure.Data;
using LearnScapeInfrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace LearnScapeAPI.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var redisConnectionString = config.GetConnectionString("Redis");

            services.AddDbContext<StoreContext>(x => x.UseSqlite(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IBasketRepo, BasketRepo>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                if (redisConnectionString == null)
                {
                    return ConnectionMultiplexer.Connect("localhost");
                }
                else
                {
                    var config = ConfigurationOptions.Parse(redisConnectionString, true);
                    return ConnectionMultiplexer.Connect(config);
                }
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            return services;
        }
    }
}
