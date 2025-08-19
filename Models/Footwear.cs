﻿namespace eCommerce_backend.Models
{
    public class Footwear
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Color { get; set; } = string.Empty;

        public string Size { get; set; } = string.Empty;

        public int Stock { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
