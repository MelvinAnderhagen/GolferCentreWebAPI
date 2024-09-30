using System;
using System.Collections.Generic;

namespace GolferCentreWebAPI.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public bool? IsActive { get; set; }
}
