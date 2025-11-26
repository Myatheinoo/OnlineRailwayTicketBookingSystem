using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class Station
{
    public long Id { get; set; }

    public Guid StationId { get; set; }

    public long RegionId { get; set; }

    public long TownshipId { get; set; }

    public string StationNo { get; set; } = null!;

    public decimal? Lat { get; set; }

    public decimal? Long { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool ActiveFlag { get; set; }

    public string? Name { get; set; }
}
