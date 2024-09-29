using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.Id)
                .UseHiLo("order_hilo");

            builder.Property(p => p.UserId)
                .IsRequired();
        }
    }
}
