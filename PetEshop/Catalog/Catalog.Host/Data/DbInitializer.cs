using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.CatalogTypes.Any())
        {
            await context.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

            await context.SaveChangesAsync();
        }


        if (!context.Users.Any())
        {
            await context.Users.AddRangeAsync(GetPreconfigurationUser());
            await context.SaveChangesAsync();
        }


        if (!context.Orders.Any())
        {
            await context.Orders.AddRangeAsync(GetPreconfigurationOrder());
            await context.SaveChangesAsync();
        }


        if (!context.OrderItems.Any())
        {
            await context.OrderItems.AddRangeAsync(GetPreconfigurationOrderItem());
            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<CatalogTypeEntity> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogTypeEntity>()
        {
            new CatalogTypeEntity() { Type = "Fruit" },
            new CatalogTypeEntity() { Type = "Nut" },
            new CatalogTypeEntity() { Type = "Mushroom" },
            new CatalogTypeEntity() { Type = "Vegatable" }
        };
    }

    private static IEnumerable<UserEntity> GetPreconfigurationUser()
    {
        return new List<UserEntity>()
            {
                new UserEntity { Mail = "Test@gmail.com" }
            };
    }
    private static IEnumerable<OrderCatalogItemEntity> GetPreconfigurationOrderItem()
    {
        return new List<OrderCatalogItemEntity>()
            {
                new OrderCatalogItemEntity {  Count = 2,  CatalogItemId = 1, OrderId = 1 },
                new OrderCatalogItemEntity {  Count = 3,  CatalogItemId = 2, OrderId = 1 },
            };
    }

    private static OrderEntity GetPreconfigurationOrder()
    {
        return new OrderEntity()
        {
            UserId = 1,
        };
    }

    private static IEnumerable<CatalogItemEntity> GetPreconfiguredItems()
    {
        return new List<CatalogItemEntity>()
        {
            new CatalogItemEntity { CatalogTypeId = 1, AvailableStock = 100, Description = "strawberry.discription", Name = "strawberry", Price = 19.5M, PictureFileName = "strawberry.png" },
            new CatalogItemEntity { CatalogTypeId = 1, AvailableStock = 100, Description = "bilberry.discription", Name = "bilberry", Price = 8.50M, PictureFileName = "bilberry.png" },
            new CatalogItemEntity { CatalogTypeId = 1, AvailableStock = 100, Description = "orange.discription", Name = "orange", Price = 12, PictureFileName = "orange.png" },
            new CatalogItemEntity { CatalogTypeId = 1, AvailableStock = 100, Description = "lemon.discription", Name = "lemon", Price = 12, PictureFileName = "lemon.png" },
            new CatalogItemEntity { CatalogTypeId = 1, AvailableStock = 100, Description = "mango.discription", Name = "mango", Price = 8.5M, PictureFileName = "mango.png" },
            new CatalogItemEntity { CatalogTypeId = 1, AvailableStock = 100, Description = "kiwi.discription", Name = "kiwi", Price = 12, PictureFileName = "kiwi.png" },
            new CatalogItemEntity { CatalogTypeId = 1, AvailableStock = 100, Description = "coconut.discription", Name = "coconut", Price = 12, PictureFileName = "coconut.png" },
            new CatalogItemEntity { CatalogTypeId = 1, AvailableStock = 100, Description = "lime.discription", Name = "lime", Price = 8.5M, PictureFileName = "lime.png" },
            new CatalogItemEntity { CatalogTypeId = 1, AvailableStock = 100, Description = "apple.discription", Name = "apple", Price = 12, PictureFileName = "apple.png" },
            new CatalogItemEntity { CatalogTypeId = 2, AvailableStock = 100, Description = "peanut.discription", Name = "peanut", Price = 12, PictureFileName = "peanut.png" },
            new CatalogItemEntity { CatalogTypeId = 3, AvailableStock = 100, Description = "mushroom.discription", Name = "mushroom", Price = 8.5M, PictureFileName = "mushroom.png" },
            new CatalogItemEntity { CatalogTypeId = 4, AvailableStock = 100, Description = "corn.discription", Name = "corn", Price = 12, PictureFileName = "corn.png" },
        };
    }
}