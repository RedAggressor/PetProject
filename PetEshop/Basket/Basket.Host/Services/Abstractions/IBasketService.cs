using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;

namespace Basket.Host.Services.Abstractions;

public interface IBasketService
{    
    Task<GetDataResponse<ItemDto>> GetItemsAsync(string userId);
    Task<BaseResponse> AddItemsAsync(string userId, ICollection<ItemDto> data);
}
