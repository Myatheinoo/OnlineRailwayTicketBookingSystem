using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public byte ActiveFlag { get; set; }
}
