﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CTQM__CAR_API.CTQM_CAR.Domain;

public partial class CarDetail
{
    public Guid DetailId { get; set; }

    public Guid CarId { get; set; }

    public string Head1 { get; set; }

    public string Title1 { get; set; }

    public string Head2 { get; set; }

    public string Title2 { get; set; }

    public string Head3 { get; set; }

    public string Title3 { get; set; }

    public virtual Car Car { get; set; }
}