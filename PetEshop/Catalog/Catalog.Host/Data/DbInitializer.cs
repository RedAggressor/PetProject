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
    }

    private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>()
        {
            new CatalogType() { Type = "Fruit" },
            new CatalogType() { Type = "Nut" },
            new CatalogType() { Type = "Mushroom" },
            new CatalogType() { Type = "Vegatable" }
        };
    }

    private static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>()
        {
            new CatalogItem { CatalogTypeId = 1, AvailableStock = 100, Description = "strawberry.discription", Name = "strawberry", Price = 19.5M, PictureFileName = "strawberry.png" },
            new CatalogItem { CatalogTypeId = 1, AvailableStock = 100, Description = "bilberry.discription", Name = "bilberry", Price = 8.50M, PictureFileName = "bilberry.png" },
            new CatalogItem { CatalogTypeId = 1, AvailableStock = 100, Description = "orange.discription", Name = "orange", Price = 12, PictureFileName = "orange.png" },
            new CatalogItem { CatalogTypeId = 1, AvailableStock = 100, Description = "lemon.discription", Name = "lemon", Price = 12, PictureFileName = "lemon.png" },
            new CatalogItem { CatalogTypeId = 1, AvailableStock = 100, Description = "mango.discription", Name = "mango", Price = 8.5M, PictureFileName = "mango.png" },
            new CatalogItem { CatalogTypeId = 1, AvailableStock = 100, Description = "kiwi.discription", Name = "kiwi", Price = 12, PictureFileName = "kiwi.png" },
            new CatalogItem { CatalogTypeId = 1, AvailableStock = 100, Description = "coconut.discription", Name = "coconut", Price = 12, PictureFileName = "coconut.png" },
            new CatalogItem { CatalogTypeId = 1, AvailableStock = 100, Description = "lime.discription", Name = "lime", Price = 8.5M, PictureFileName = "lime.png" },
            new CatalogItem { CatalogTypeId = 1, AvailableStock = 100, Description = "apple.discription", Name = "apple", Price = 12, PictureFileName = "apple.png" },
            new CatalogItem { CatalogTypeId = 2, AvailableStock = 100, Description = "peanut.discription", Name = "peanut", Price = 12, PictureFileName = "peanut.png" },
            new CatalogItem { CatalogTypeId = 3, AvailableStock = 100, Description = "mushroom.discription", Name = "mushroom", Price = 8.5M, PictureFileName = "mushroom.png" },
            new CatalogItem { CatalogTypeId = 4, AvailableStock = 100, Description = "corn.discription", Name = "corn", Price = 12, PictureFileName = "corn.png" },
        };
    }
}