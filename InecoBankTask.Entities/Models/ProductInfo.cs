﻿namespace InecoBankTask.Entities.Models;

public partial class ProductInfo
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }
}