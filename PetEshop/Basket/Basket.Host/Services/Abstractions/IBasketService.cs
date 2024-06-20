using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;

namespace Basket.Host.Services.Abstractions;

public interface IBasketService
{    
    Task<TestGetResponse<ItemRequest>> GetItems(string userId);
    Task UpdateItems(string userId, ICollection<ItemRequest> data);
    Task DeleteItem(string userId, int idItem);
    Task AddItem(string userId, ICollection<ItemRequest> data);
}
