﻿using System;
using System.Collections.Generic;

namespace CTQM_CAR.Domain;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid CarId { get; set; }

    public DateTime OrderDate { get; set; }

    public string? OrderStatus { get; set; }

    public int Amount { get; set; }

    public double TotalPrice { get; set; }

    public Guid CustomerId { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
