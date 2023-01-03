using Core.BusinessModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<ProductBM> Products { get; set; }
        public DbSet<ProductBrandBM> ProductBrands { get; set; }
        public DbSet<ProductTypeBM> ProductTypes { get; set; }
        public DbSet<UserBM> Users { get; set; }
        public DbSet<AddressBM> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
