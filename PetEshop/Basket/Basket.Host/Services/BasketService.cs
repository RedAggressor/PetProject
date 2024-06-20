using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;
using Basket.Host.Services.Abstractions;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;
    private const string _key = "basket";
    public BasketService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<TestGetResponse<ItemRequest>> GetItems(string userId)
    {
        string key = $"{_key}{userId}";
        var result = await _cacheService.GetAsync<ICollection<ItemRequest>>(key);
        return new TestGetResponse<ItemRequest>() { Data = result };
    }

    public async Task UpdateItems(string userId, ICollection<ItemRequest> data)
    {
        string key = $"{_key}{userId}";
        await _cacheService.AddOrUpdateAsync(key, data);   
    }

    public async Task DeleteItem(string userId, int idItem)
    {
        string key = $"{_key}{userId}";
        var result = await _cacheService.GetAsync<ICollection<ItemRequest>>(key);
        var corection = -1; // becose intex start 0, item start 1;
        result.ToList().RemoveAt(idItem - corection);
        await _cacheService.AddOrUpdateAsync(key, result);
    }

    public async Task AddItem(string userId, ICollection<ItemRequest> data)
    {
        string key = $"{_key}{userId}";
        var result = await _cacheService.GetAsync<ICollection<ItemRequest>>(key);
        result.ToList().AddRange(data);
        await _cacheService.AddOrUpdateAsync(key, result);
    }
}
