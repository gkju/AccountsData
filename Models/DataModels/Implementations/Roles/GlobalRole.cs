using System;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Implementations.RoleScope;
using AccountsData.Models.DataModels.RoleProperties;

namespace AccountsData.Models.DataModels.Implementations.Roles
{
    public class GlobalRole : Role
    {
        public string Name { get; set; }
        public GlobalRole(DataModels.Properties properties, ApplicationUser author, string Name)
        {
            scope = new GlobalScope();
            this.Name = Name;
            Id = Guid.NewGuid();
            int authorAuthority = author.GetAuthority(scope);
            this.properties = new GenericRoleProperties(properties, authorAuthority);
        }
    }
}