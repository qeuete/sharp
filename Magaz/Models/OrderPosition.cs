using System;
using System.Collections.Generic;

namespace Magaz.Models;

public partial class OrderPosition
{
    public int IdOrderPosition { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int CountProduct { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
