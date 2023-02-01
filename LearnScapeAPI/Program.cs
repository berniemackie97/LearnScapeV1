using Infrastructure.Data;
using LearnScapeAPI.Extensions;
using LearnScapeAPI.Helpers;
using LearnScapeAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(connectionString));
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
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
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MappingProfiles));
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
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context, loggerFactory);

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

app.UseAuthorization();

app.MapControllers();

SeedDataBase();