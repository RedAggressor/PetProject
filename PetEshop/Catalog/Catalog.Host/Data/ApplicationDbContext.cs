using Catalog.Host.Data.Entities;
using Catalog.Host.Data.EntityConfigurations;

namespace Catalog.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<CatalogItemEntity> CatalogItems { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<OrderCatalogItemEntity> OrderItems { get; set; } = null!;
        public DbSet<CatalogTypeEntity> CatalogTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderCatalogItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}
