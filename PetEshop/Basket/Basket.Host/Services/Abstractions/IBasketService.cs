using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;

namespace Basket.Host.Services.Abstractions;

public interface IBasketService
{    
    Task<TestGetResponse<ItemRequest>> GetItems(string userId);
    Task AddItems(string userId, ICollection<ItemRequest> data);    
}
