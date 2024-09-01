using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseHiLo("order_item_hilo")
                .IsRequired();

            builder.HasOne(x => x.Item)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(f => f.ItemId);

            builder.HasOne(o => o.Order)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(f => f.OrderId);

        }
    }
}
