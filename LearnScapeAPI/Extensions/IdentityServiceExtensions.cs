using LearnScapeCore.BusinessModels.identity;
using LearnScapeInfrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Text;

namespace LearnScapeAPI.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddSignInManager<SignInManager<AppUser>>();


            // Authentication always comes before Authorization
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.ForwardForbid = JwtBearerDefaults.AuthenticationScheme;
                        options.ForwardAuthenticate = JwtBearerDefaults.AuthenticationScheme;
                        options.ForwardChallenge = JwtBearerDefaults.AuthenticationScheme;
                        options.Challenge = JwtBearerDefaults.AuthenticationScheme;
                        options.ForwardDefault = JwtBearerDefaults.AuthenticationScheme;
                        options.Events = new JwtBearerEvents
                        {
                            OnChallenge = context =>
                            {
                                context.HandleResponse();
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(new
                                {
                                    message = "Unauthorized"
                                }));
                            }
                        };
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                            ValidIssuer = config["Token:Issuer"],
                            ValidateAudience = false,
                            ValidateIssuer = true
                        };
                       
                    });

            services.AddAuthorization();

            return services;
        }
    }
}
