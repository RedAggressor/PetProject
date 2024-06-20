﻿namespace Basket.Host.Models.Requests
{
    public class ItemRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public int CatalogTypeId { get; set; }
        public int AvailableStock { get; set; }
    }
}