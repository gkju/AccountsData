using System;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Implementations.RoleScope;
using AccountsData.Models.DataModels.RoleProperties;

namespace AccountsData.Models.DataModels.Implementations.Roles
{
    public class GlobalRole : Role
    {
        public string Name { get; set; }

        public GlobalRole()
        {
            
        }
        public GlobalRole(DataModels.Properties properties, ApplicationUser author, string Name)
        {
            Scope = new GlobalScope();
            this.Name = Name;
            Id = Guid.NewGuid();
            int authorAuthority = author.GetAuthority(Scope);
            this.Properties = new GenericRoleProperties(properties, authorAuthority);
        }
    }
}