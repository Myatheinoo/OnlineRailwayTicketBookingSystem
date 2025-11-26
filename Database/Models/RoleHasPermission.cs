using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class RoleHasPermission
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public bool IsChecked { get; set; }
}
