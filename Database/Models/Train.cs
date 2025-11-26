using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class Train
{
    public long Id { get; set; }

    public Guid? TrainId { get; set; }

    public string? TrainName { get; set; }

    public string? TrainNo { get; set; }

    public long? TrainTypeId { get; set; }

    public DateTime? StartedUsingDate { get; set; }

    public string? Power { get; set; }

    public string? ManufactureBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? ActiveFlag { get; set; }
}
