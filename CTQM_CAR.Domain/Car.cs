using System;
using System.Collections.Generic;

namespace CTQM_CAR.Domain;

public partial class Car
{
    public Guid CarId { get; set; }

    public string CarName { get; set; } = null!;

    public string CarModel { get; set; } = null!;

    public string CarClass { get; set; } = null!;

    public string CarEngine { get; set; } = null!;

    public int CarAmount { get; set; }

    public double CarPrice { get; set; }

    public string? MoTa { get; set; }

    public string? Head1 { get; set; }

    public string? MoTa2 { get; set; }

    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
