using System;
using System.Collections.Generic;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Implementations.Properties;

namespace AccountsData.Models.DataModels.RoleProperties
{
    public class MemberRoleProperties : Properties
    {
        private void Initialize()
        {
            _propertyDictionary = new Dictionary<string, Property>()
            {
                {AuthorityProperty.Name, new AuthorityProperty(1)},
                {MemberProperty.Name, new MemberProperty()}
            };  
            Count = _propertyDictionary.Count;
        } 

        public MemberRoleProperties(Properties properties, int userAuthority = 0)
        {
            Initialize();
            
            List<string> keys = new List<string>(_propertyDictionary.Keys);

            foreach (var key in keys)
            {
                Merge(properties[key]);
            }

            this[AuthorityProperty.Name] = new AuthorityProperty(Math.Min(userAuthority, ((AuthorityProperty) this[AuthorityProperty.Name]).GetValue()));
        }
        
        public MemberRoleProperties(int userAuthority = 0, params Property[] parameters)
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