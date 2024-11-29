using System;
using System.Collections.Generic;

namespace Magaz.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string LoginUs { get; set; } = null!;

    public string PasswordUs { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
