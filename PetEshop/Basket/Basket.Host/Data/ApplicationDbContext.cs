using Basket.Host.Data.Configurations;
using Basket.Host.Data.Entities;
using Basket.Host.Models.Responces;
using Microsoft.EntityFrameworkCore;

namespace Basket.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<OrderEntity> Orders { get; set; } = null!;
        //public DbSet<ItemEntity> Items { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        }

    }
}
