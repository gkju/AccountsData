using System;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Implementations.Properties;

namespace AccountsData.Models.DataModels.Implementations.Roles
{
    public class BannedRole : Role
    {
        public new string Name = "Banned";

        public override bool UserManageable { get; } = false;

        public void Initialize()
        {
            properties = new DataModels.Properties(new BannedProperty());
        }

        public BannedRole()
        {
            
        }

        public BannedRole(Scope scope)
        {
            Id = Guid.NewGuid();
            this.scope = scope;
            Initialize();
        }
    }
}