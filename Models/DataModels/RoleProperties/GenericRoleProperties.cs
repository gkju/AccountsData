using System;
using System.Collections.Generic;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Implementations.Properties;

namespace AccountsData.Models.DataModels.RoleProperties
{
    public class GenericRoleProperties : Properties
    {
        private void Initialize()
        {
            PropertyDictionary = new Dictionary<string, Property>()
            {
                {AdminProperty.Name, new AdminProperty(false)},
                {AuthorityProperty.Name, new AuthorityProperty(0)},
                {BanUsersProperty.Name, new BanUsersProperty(false)},
                {EditOrDeleteProperty.Name, new EditOrDeleteProperty(false)},
                {MayManageRolesProperty.Name, new MayManageRolesProperty(false)},
                {Prefixes.Name, new Prefixes()},
                {ReadProperty.Name, new ReadProperty(false)},
                {ViewProperty.Name, new ViewProperty(false)},
                {WriteProperty.Name, new WriteProperty(false)},
            };
            Count = PropertyDictionary.Count;
        } 

        public GenericRoleProperties(Properties properties, int userAuthority = 0)
        {
            Initialize();
            
            List<string> keys = new List<string>(PropertyDictionary.Keys);

            foreach (var key in keys)
            {
                Merge(properties[key]);
            }

            this[AuthorityProperty.Name] = new AuthorityProperty(Math.Min(userAuthority, ((AuthorityProperty) this[AuthorityProperty.Name]).GetValue()));
        }
        
        public GenericRoleProperties(int userAuthority = 0, params Property[] parameters)
        {
            Initialize();

            foreach (var parameter in parameters)
            {
                Merge(parameter);
            }
            
            this[AuthorityProperty.Name] = new AuthorityProperty(Math.Min(userAuthority, ((AuthorityProperty) this[AuthorityProperty.Name]).GetValue()));
        }
    }
}