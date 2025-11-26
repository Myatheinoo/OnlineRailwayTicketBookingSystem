using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class TrainType
{
    public long Id { get; set; }

    public Guid TrainTypeId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool ActiveFlag { get; set; }
}
