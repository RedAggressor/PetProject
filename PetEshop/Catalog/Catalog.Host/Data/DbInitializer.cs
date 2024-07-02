using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.Types.Any())
        {
            await context.Types.AddRangeAsync(GetPreconfiguredCatalogTypes());

            await context.SaveChangesAsync();
        }

        if (!context.Items.Any())
        {
            await context.Items.AddRangeAsync(GetPreconfiguredItems());

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

    private static IEnumerable<TypeEntity> GetPreconfiguredCatalogTypes()
    {
        return new List<TypeEntity>()
        {
            new TypeEntity() { Type = "Fruit" },
            new TypeEntity() { Type = "Nut" },
            new TypeEntity() { Type = "Mushroom" },
            new TypeEntity() { Type = "Vegatable" }
        };
    }
    
    private static IEnumerable<OrderItemEntity> GetPreconfigurationOrderItem()
    {
        return new List<OrderItemEntity>()
            {
                new OrderItemEntity {  Count = 2,  ItemId = 1, OrderId = 1 },
                new OrderItemEntity {  Count = 3,  ItemId = 2, OrderId = 1 },
            };
    }

    private static OrderEntity GetPreconfigurationOrder()
    {
        return new OrderEntity()
        {
            UserId = 1,
        };
    }

    private static IEnumerable<ItemEntity> GetPreconfiguredItems()
    {
        return new List<ItemEntity>()
        {
            new ItemEntity { TypeId = 1, AvailableStock = 100, Description = "strawberry.discription", Name = "strawberry", Price = 19.5M, PictureFileName = "strawberry.png" },
            new ItemEntity { TypeId = 1, AvailableStock = 100, Description = "bilberry.discription", Name = "bilberry", Price = 8.50M, PictureFileName = "bilberry.png" },
            new ItemEntity { TypeId = 1, AvailableStock = 100, Description = "orange.discription", Name = "orange", Price = 12, PictureFileName = "orange.png" },
            new ItemEntity { TypeId = 1, AvailableStock = 100, Description = "lemon.discription", Name = "lemon", Price = 12, PictureFileName = "lemon.png" },
            new ItemEntity { TypeId = 1, AvailableStock = 100, Description = "mango.discription", Name = "mango", Price = 8.5M, PictureFileName = "mango.png" },
            new ItemEntity { TypeId = 1, AvailableStock = 100, Description = "kiwi.discription", Name = "kiwi", Price = 12, PictureFileName = "kiwi.png" },
            new ItemEntity { TypeId = 1, AvailableStock = 100, Description = "coconut.discription", Name = "coconut", Price = 12, PictureFileName = "coconut.png" },
            new ItemEntity { TypeId = 1, AvailableStock = 100, Description = "lime.discription", Name = "lime", Price = 8.5M, PictureFileName = "lime.png" },
            new ItemEntity { TypeId = 1, AvailableStock = 100, Description = "apple.discription", Name = "apple", Price = 12, PictureFileName = "apple.png" },
            new ItemEntity { TypeId = 2, AvailableStock = 100, Description = "peanut.discription", Name = "peanut", Price = 12, PictureFileName = "peanut.png" },
            new ItemEntity { TypeId = 3, AvailableStock = 100, Description = "mushroom.discription", Name = "mushroom", Price = 8.5M, PictureFileName = "mushroom.png" },
            new ItemEntity { TypeId = 4, AvailableStock = 100, Description = "corn.discription", Name = "corn", Price = 12, PictureFileName = "corn.png" },
        };
    }
}