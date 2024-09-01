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
        public DbSet<ItemEntity> Items { get; set; } = null!;        
        public DbSet<OrderItemEntity> OrderItems { get; set; } = null!;
        public DbSet<TypeEntity> Types { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ItemEntityConfiguration());           
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TypeEntityConfiguration());
        }
    }
}
