﻿namespace HangangRamyeon.Domain.Entities;
public class ProductImage
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}

