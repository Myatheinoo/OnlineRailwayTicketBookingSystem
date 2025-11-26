using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expire { get; set; }

    public int UserId { get; set; }
}
