using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseHiLo("user_hilo")
                .IsRequired();
        }
    }
}
