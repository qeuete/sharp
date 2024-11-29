using System;
using System.Collections.Generic;

namespace Magaz.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int? UserId { get; set; }

    public DateTime Date { get; set; }

    public decimal TotalSum { get; set; }

    public virtual ICollection<OrderPosition> OrderPositions { get; set; } = new List<OrderPosition>();

    public virtual User? User { get; set; }
}
