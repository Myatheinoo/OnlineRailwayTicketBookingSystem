using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class SubRoute
{
    public long Id { get; set; }

    public Guid SubRouteId { get; set; }

    public decimal UpperClassFee { get; set; }

    public decimal OrdinaryClassFee { get; set; }

    public decimal FirstClassFee { get; set; }

    public decimal SleeperClassFee { get; set; }

    public long MainRouteId { get; set; }

    public decimal LifeInsurance { get; set; }

    public string? Distance { get; set; }

    public string? Duration { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool ActiveFlag { get; set; }
}
