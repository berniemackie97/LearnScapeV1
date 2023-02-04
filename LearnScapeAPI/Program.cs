using Infrastructure.Data;
using LearnScapeAPI.Extensions;
using LearnScapeAPI.Helpers;
using LearnScapeAPI.Middleware;
using LearnScapeCore.BusinessModels.identity;
using LearnScapeInfrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

async void SeedDataBase()
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = services.GetRequiredService<StoreContext>();
            var identityContext = services.GetRequiredService<AppIdentityDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context, loggerFactory);

            await identityContext.Database.MigrateAsync();
            await AppIdentityDbContextSeed.SeedAUserAsync(userManager);

        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }
    app.Run();
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

SeedDataBase();