using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class Region
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string NameMm { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
