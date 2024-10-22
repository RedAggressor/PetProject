using Catalog.Host.Models.enums;

namespace Catalog.Host.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string Status { get; set; } = StatusType.Empty.ToString();
    public string PayStatus { get; set; } = PayStatusType.None.ToString();
    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}

