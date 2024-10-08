﻿using Catalog.Host.Models.enums;

namespace Catalog.Host.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public StatusType Status { get; set; } = StatusType.Empty;
    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}

