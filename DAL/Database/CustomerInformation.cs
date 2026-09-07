using System;
using System.Collections.Generic;

namespace DAL.Database;

public partial class CustomerInformation
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime Createdon { get; set; }

    public int? RoleId { get; set; }
}
