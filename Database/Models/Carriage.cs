using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class Carriage
{
    public long Id { get; set; }

    public Guid CarriageId { get; set; }

    public string TypeMm { get; set; } = null!;

    public string TypeEn { get; set; } = null!;

    public long TrainTypeId { get; set; }

    public int Seat { get; set; }

    public bool IsBv { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool AcitveFlag { get; set; }
}
