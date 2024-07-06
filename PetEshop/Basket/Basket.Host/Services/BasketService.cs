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

    public async Task<GetDataResponse<ItemDto>> GetItems(string userId)
    {
        string key = $"{_key}{userId}";
        var result = await _cacheService.GetAsync<ICollection<ItemDto>>(key);
        return new GetDataResponse<ItemDto>() { Data = result };
    }

    public async Task AddItems(string userId, ICollection<ItemDto> data)
    {
        string key = $"{_key}{userId}";
        await _cacheService.AddOrUpdateAsync(key, data);   
    }
}
