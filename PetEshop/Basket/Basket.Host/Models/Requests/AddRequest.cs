﻿namespace Basket.Host.Models.Requests
{
    public class AddRequest
    {
        public string UserId { get; set; } = null!;
        public ICollection<ItemDto> Items { get; set; } = null!;
    }
}