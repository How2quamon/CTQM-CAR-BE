using System;
using System.Collections.Generic;

namespace CTQM_CAR.Domain;

public partial class Cart
{
    public Guid CartId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid CarId { get; set; }

    public int Amount { get; set; }

    public double Price { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
