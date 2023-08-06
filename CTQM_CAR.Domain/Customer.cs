using System;
using System.Collections.Generic;

namespace CTQM_CAR.Domain;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerPhone { get; set; } = null!;

    public string? CustomerAddress { get; set; }

    public DateTime CustomerDate { get; set; }

    public string CustomerLicense { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string? CustomerPassword { get; set; }

    public bool? CustomerVaild { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
