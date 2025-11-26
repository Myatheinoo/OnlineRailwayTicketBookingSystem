using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class MainRoute
{
    public int Id { get; set; }

    public Guid MainRouteId { get; set; }

    public string? RouteType { get; set; }

    public string? Duration { get; set; }

    public int TrainTypeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? ActiveFlag { get; set; }

    public string? RouteName { get; set; }
}
