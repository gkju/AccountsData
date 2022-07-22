using System;
using AccountsData.Models.DataModels.Abstracts;

namespace AccountsData.Models.DataModels.Implementations.Roles;

public class UserRole
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Role role { get; set; }
    public DateTime expiresAt { get; set; }
    public bool expire { get; set; }
}