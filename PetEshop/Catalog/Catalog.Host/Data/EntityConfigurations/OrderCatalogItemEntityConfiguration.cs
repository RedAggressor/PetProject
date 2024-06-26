using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class OrderCatalogItemEntityConfiguration : IEntityTypeConfiguration<OrderCatalogItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderCatalogItemEntity> builder)
        {
            builder.ToTable("OrderCatalogItem");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseHiLo("order_catalog_item_hilo")
                .IsRequired();

            builder.HasOne(x => x.CatalogItem)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(f => f.CatalogItemId);

            builder.HasOne(o => o.Order)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(f => f.OrderId);

        }
    }
}
