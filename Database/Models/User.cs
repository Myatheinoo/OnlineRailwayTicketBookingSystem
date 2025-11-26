using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public byte ActiveFlag { get; set; }

    public string? PasswordHash { get; set; }

    public string? SaltKey { get; set; }

    public string? Token { get; set; }
}
