using System;
using System.Collections.Generic;

namespace Magaz.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string NameProduct { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public string? ProductUrl { get; set; }

    public decimal Price { get; set; }

    public int? CategoryId { get; set; }

    public bool IsFavorite { get; set; }

    public int CartQuantity { get; set; }

    public string Size { get; set; } = null!;

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderPosition> OrderPositions { get; set; } = new List<OrderPosition>();
}
