﻿using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Requests
{
    public class OrderItemRequest
    {
        public int Count { get; set; }
        public int ItemId { get; set; }
        public CatalogItemDto Item { get; set; } = null!;
    }
}
